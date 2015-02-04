using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Models.RSS;
using Models.User;
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

        public UserItemsViewModel GetUserChannelItems(long userChannelId, string userId, int viewType)
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
                completeItemView.ViewDisplay = GetViewDisplay(viewType);
                itemList.Add(completeItemView);
            }

            userItemsViewModel.Items = itemList.OrderByDescending(item => item.PublishDate).ToList();
            userItemsViewModel.LastPost = userItemsViewModel.Items[0].ItemAge;
            userItemsViewModel.TotalPosts = userItemsViewModel.Items.Count;

            userItemsViewModel.PostsPerDay = CalculatePostsPerDay(userItemsViewModel.Items.First().PublishDate, userItemsViewModel.Items.Last().PublishDate, userItemsViewModel.TotalPosts);

            return userItemsViewModel;
        }

        private UserCustomView GetViewDisplay(int viewType)
        {
            var result = new UserCustomView();
            switch (viewType)
            {
                case 1:
                    result = DefaultViews.Simple;
                    break;
                case 2:
                    result = DefaultViews.Full;
                    break;
            }
            return result;
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

            var allUserItemViewModelSorted = allUserItemsViewModel.OrderByDescending( x => x.PublishDate).ToList();

            return allUserItemViewModelSorted;
        }

        public List<ShowAllUserItemsViewModel> GetAllUnreadUserItems(string userId)
        {
            var allUserItems = GetAllUserItems(userId);
            var allUnreadUserItemsViewModel = allUserItems.Where(x => x.Read == false).ToList();

            return allUnreadUserItemsViewModel;
        }
        
        public bool IncreaseUserRating(long userItemId)
        {
            var response = false;
            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    response = IncreaseItemRating(userItemId);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            return response;

        }
        
        public bool DecreaseUserRating(long userItemId)
        {
            var response = false;
            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    response = DecreaseItemRating(userItemId);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            return response;
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

        public string CalculateItemAge(DateTime publishTime)
        {
            var timeSpan = DateTime.Now - publishTime;
            string result;
            var days = timeSpan.Days;
            var hours = timeSpan.Hours;
            var minutes = timeSpan.Minutes;
            switch (days)
            {
                case 0:
                    result = string.Format("{0} h {1} min", hours, minutes);
                    break;
                case 1:
                    result = string.Format("1 day {0} h {1} min", hours, minutes);
                    break;
                default:
                    result = string.Format("{0} days {1} h {2} min", days, hours, minutes);
                    break;
            }
            return result;
        }

        private bool IncreaseItemRating(long userItemId)
        {
            var response = false;
            var userItem =
                _rssDatabase.UsersItems
                    .FirstOrDefault(querry => querry.Id == userItemId);
            var item =
                _rssDatabase.AllItems
                    .FirstOrDefault(querry => querry.Id == userItem.ItemId);
            response = CheckIfThisIsUserFirstSelection(userItem, response);
            if (userItem.RatingMinus)
            {
                userItem.RatingMinus = false;
                item.RatingMinus--;
            }
            userItem.RatingPlus = true;
            item.RatingPlus++;

            _rssDatabase.SaveChanges();
            return response;
        }

        private bool DecreaseItemRating(long userItemId)
        {
            var response = false;

            var userItem =
                _rssDatabase.UsersItems.FirstOrDefault(querry => querry.Id == userItemId);
            var item =
                _rssDatabase.AllItems.FirstOrDefault(querry => querry.Id == userItem.ItemId);
            response = CheckIfThisIsUserFirstSelection(userItem, response);
            if (userItem.RatingPlus)
            {
                userItem.RatingPlus = false;
                item.RatingPlus--;
            }
            userItem.RatingMinus = true;
            item.RatingMinus++;

            _rssDatabase.SaveChanges();
            return response;
        }

        private bool CheckIfThisIsUserFirstSelection(UserItem userItem, bool response)
        {
            if (userItem.RatingPlus == true ^ userItem.RatingMinus == true)
            {
                response = true;
            }
            return response;
        }

        private double CalculatePostsPerDay(DateTime firstPostPublish, DateTime lastPostPublish, long totalPosts)
        {
            var timeSpan = (firstPostPublish - lastPostPublish).Days;
            if (timeSpan == 0)
            {
                timeSpan = 1;
            }
            double result = Math.Round((double)totalPosts / timeSpan, 1);
            return result;
        }
    }
}
