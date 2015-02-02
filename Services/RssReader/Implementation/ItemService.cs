﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Models.RSS;
using Models.ViewModels;
using RssDataContext;

namespace Services.RssReader.Implementation
{
    public class ItemService : IItemService
    {

        private readonly IApplicationRssDataContext _rssDatabase;

        public ItemService(IApplicationRssDataContext rssDatabase)
        {
            _rssDatabase = rssDatabase;
        }

        public Channel GetChannelWithItems(long channelId)
        {
            var channel = _rssDatabase.Channels
                .Include(x => x.Items)
                .First(x => x.Id == channelId);
 
            return channel;
        }

        public UserItemsViewModel GetUserChannelItems(long userChannelId, string userId)
        {
            var itemsAndChannel = _rssDatabase.UserChannels
                .Include(x => x.Channel)
                .Include(x => x.UserItems)
                .Include(x => x.Channel.Items)
                .Where(userChannel => userChannel.ApplicationUserId == userId)
                .First(userChannel => userChannel.Id == userChannelId);

            var userItemsViewModel = Mapper.Map<Channel, UserItemsViewModel>(itemsAndChannel.Channel);
            Mapper.Map<UserChannel, UserItemsViewModel>(itemsAndChannel, userItemsViewModel);

            var itemList = new List<CompleteItemInfo>();

            foreach (var userItem in itemsAndChannel.UserItems)
            {
                var completeItemView = Mapper.Map<UserItem, CompleteItemInfo>(userItem);
                Mapper.Map<Item, CompleteItemInfo>(userItem.Item, completeItemView);

                completeItemView.ItemAge = CalculateItemAge(completeItemView.PublishDate);
                itemList.Add(completeItemView);
            }

            userItemsViewModel.Items = itemList.OrderBy(item => item.ItemAge).ToList();

            return userItemsViewModel;
        }

        public List<ShowAllUserItemsViewModel> GetAllUserItems(string userId)
        {
            var userItems = _rssDatabase.UsersItems
                .Include(x => x.Item)
                .Include(x => x.UserChannel.Channel)
                .Where(userItem => userItem.ApplicationUserId == userId).ToList();

            var allUserItemsViewModel = new List<ShowAllUserItemsViewModel>();

            foreach (UserItem userItem in userItems)
            {
                var userItemViewModel = Mapper.Map<UserItem, ShowAllUserItemsViewModel>(userItem);
                Mapper.Map<Item, ShowAllUserItemsViewModel>(userItem.Item, userItemViewModel);
                Mapper.Map<Channel, ShowAllUserItemsViewModel>(userItem.UserChannel.Channel, userItemViewModel);
                userItemViewModel.ItemAge = CalculateItemAge(userItemViewModel.PublishDate);
                
                allUserItemsViewModel.Add(userItemViewModel);
            }

            var allUserItemViewModelSorted = allUserItemsViewModel.OrderBy(x => x.ItemAge).ToList();

            return allUserItemViewModelSorted;
        }

        public List<ShowAllUserItemsViewModel> GetAllUnreadUserItems(string userId)
        {
            var allUserItems = GetAllUserItems(userId);
            var allUnreadUserItemsViewModel = allUserItems.Where(x => x.Read == false).ToList();

            return allUnreadUserItemsViewModel;
        }
        
        public void IncreaseUserRating(long userItemId)
        {
            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    IncreaseItemRating(userItemId);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }
        
        public void DecreaseUserRating(long userItemId)
        {
            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    DecreaseItemRating(userItemId);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        public void MarkAsRead(long userItemId)
        {
            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                     var userItem = _rssDatabase.UsersItems
                     .Single(item => item.Id == userItemId);

                     userItem.Read = true;

                    _rssDatabase.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        public void MarkAllItemsAsRead(string userId)
        {
            var items = _rssDatabase.UsersItems
                        .Where(x => x.ApplicationUserId == userId)
                        .ToList();

            foreach (var item in items)
            {
                item.Read = true;
            }

            _rssDatabase.SaveChanges();
        }

        public void MarkAllChannelItemsAsRead(string userId, long channelId)
        {
            var items = _rssDatabase.UsersItems
                        .Where(x => x.UserChannelId == channelId)
                        .Where(x => x.ApplicationUserId == userId)
                        .ToList();

            foreach (var item in items)
            {
                item.Read = true;
            }

            _rssDatabase.SaveChanges();

        }

        public UserItem FetchUserItem(long userItemId)
        {
            return _rssDatabase.UsersItems
                .FirstOrDefault(userItem => userItem.Id == userItemId);
        }

        public TimeSpan CalculateItemAge(DateTime publishTime)
        {
            var result = DateTime.Now - publishTime;
            return result;
        }

        private void IncreaseItemRating(long userItemId)
        {
            var userItem =
                _rssDatabase.UsersItems
                    .FirstOrDefault(querry => querry.Id == userItemId);
            var item =
                _rssDatabase.AllItems
                    .FirstOrDefault(querry => querry.Id == userItem.ItemId);
            if (userItem.RatingMinus)
            {
                userItem.RatingMinus = false;
                item.RatingMinus--;
            }
            userItem.RatingPlus = true;
            item.RatingPlus++;

            _rssDatabase.SaveChanges();
        }

        private void DecreaseItemRating(long userItemId)
        {
            var userItem =
                _rssDatabase.UsersItems.FirstOrDefault(querry => querry.Id == userItemId);
            var item =
                _rssDatabase.AllItems.FirstOrDefault(querry => querry.Id == userItem.ItemId);
            if (userItem.RatingPlus)
            {
                userItem.RatingPlus = false;
                item.RatingPlus--;
            }
            userItem.RatingMinus = true;
            item.RatingMinus++;

            _rssDatabase.SaveChanges();
        }


    }
}
