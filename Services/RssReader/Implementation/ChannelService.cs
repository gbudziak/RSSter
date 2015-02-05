using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using Models.RSS;
using Models.ViewModels;
using RssDataContext;

namespace Services.RssReader.Implementation
{
    public class ChannelService : IChannelService
    {

        private readonly IApplicationRssDataContext _rssDatabase;
        private readonly IChannelGet _channelGet;

        public ChannelService(IApplicationRssDataContext rssDatabase, IChannelGet channelGet)
        {
            _rssDatabase = rssDatabase;
            _channelGet = channelGet;
        }

        public void AddChannel(string url)
        {

            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    CheckAndCreateChannel(url);
                    transaction.Commit();

                }
                catch
                {
                    transaction.Rollback();
                }
            }

        }

        public long AddChannel(string userId, string url)
        {
            var result = -1L;
            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    //CheckAndCreateChannel(url);
                    result = CreateOrRestoreUserChannel(userId, url);
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                }
            }
            return result;
        }

        public void RemoveChannel(string userId, long userChannelId)
        {
            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    var toRemove =
                        _rssDatabase.UserChannels
                            .Where(userChannel => userChannel.ApplicationUserId == userId)
                            .FirstOrDefault(userChannel => userChannel.Id == userChannelId);
                    toRemove.IsHidden = true;
                    DecreaseReadersCount(toRemove.ChannelId);
                    _rssDatabase.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        public List<ShowUserChannelsViewModel> GetUserChannels(string userId)
        {
            var userChannels = _rssDatabase.UserChannels
                .Include(x => x.Channel)
                .Include(x => x.UserItems)
                .Where(x => x.ApplicationUserId == userId)
                .Where(x => x.IsHidden == false);

            var userChannelsViewModel = new List<ShowUserChannelsViewModel>();

            foreach (var userChannel in userChannels)
            {
                var completeChannel = Mapper.Map<UserChannel, ShowUserChannelsViewModel>(userChannel);
                Mapper.Map<Channel, ShowUserChannelsViewModel>(userChannel.Channel, completeChannel);

                completeChannel.AllItemsCount = userChannel.UserItems.Count;

                var unreadUserItems = userChannel.UserItems.Where(userItem => userItem.Read == false);
                completeChannel.UnreadItemsCount = unreadUserItems.Count();

                userChannelsViewModel.Add(completeChannel);
            }

            return userChannelsViewModel;
        }

        public long ReturnChannelId(string url)
        {
            var channelId = _rssDatabase.Channels.First(channel => channel.Url == url).Id;
            return channelId;
        }

        public long ReturnUserChannelId(string url, string userId)
        {
            var userChannelId =
                _rssDatabase.UserChannels
                .Where(userChannel => userChannel.ApplicationUserId == userId)
                .FirstOrDefault(userChannel => userChannel.Channel.Url == url)
                .Id;
            return userChannelId;
        }

        public void IncreaseReadersCount(long channelId)
        {
            var channel = _rssDatabase.Channels.First(chan => chan.Id == channelId);
            channel.Readers++;
            _rssDatabase.SaveChanges();
        }

        public void DecreaseReadersCount(long channelId)
        {
            var channel = _rssDatabase.Channels.FirstOrDefault(chan => chan.Id == channelId);
            channel.Readers--;
            _rssDatabase.SaveChanges();
        }

        public void AddNewItemsToChannel(long channelId, string url)
        {
            var lastItemDateTime = GetDateTimeFromLastChannelItem(channelId);
            var channel = _channelGet.GetUpdatedRssChannel(url, lastItemDateTime, channelId);

            channel.Readers = _rssDatabase.Channels.First(x => x.Id == channelId).Readers;
            _rssDatabase.Channels.AddOrUpdate(channel);
            _rssDatabase.AllItems.AddRange(channel.Items);

            _rssDatabase.SaveChanges();
        }

        public void AddNewItemsToUserChannel(string userId, long channelId, long userChannelId)
        {
            var lastUserItemDateTime = GetDateTimeFromLastUserChannelItem(userChannelId);

            var items = _rssDatabase.AllItems
               .Where(x => x.ChannelId == channelId)
               .Where(x => x.PublishDate > lastUserItemDateTime)
               .ToList();

            foreach (var item in items)
            {
                var userItem = new UserItem(userId, item.Id, userChannelId);
                _rssDatabase.UsersItems.Add(userItem);
            }

            _rssDatabase.SaveChanges();
        }

        private long CreateOrRestoreUserChannel(string userId, string url)
        {
            var hiddenUserChannel =
                _rssDatabase.UserChannels
                    .Where(userChannel => userChannel.ApplicationUserId == userId)
                    .FirstOrDefault(userChannel => userChannel.Channel.Url == url);
            if (hiddenUserChannel != null)
            {
                if (hiddenUserChannel.IsHidden == true)
                {
                    RestoreHiddenChannel(userId, url, hiddenUserChannel);
                    return hiddenUserChannel.Id;
                }

                return hiddenUserChannel.Id;
            }
            else
            {
                var userChannelId = CreateNewUserChannel(userId, url);
                return userChannelId;
            }
        }

        private void CheckAndCreateChannel(string url)
        {
            if (_rssDatabase.Channels.Any(channelChecl => channelChecl.Url == url)) return;
            var channel = _channelGet.GetRssChannelWithFeeds(url);
            _rssDatabase.Channels.Add(channel);
            _rssDatabase.SaveChanges();
        }

        private long CreateNewUserChannel(string userId, string url)
        {
            var channelId = ReturnChannelId(url);
            IncreaseReadersCount(channelId);

            var userchannel = new UserChannel(channelId, userId);
            var items = _rssDatabase.AllItems
                .Where(x => x.ChannelId == channelId)
                .ToList();

            foreach (var item in items)
            {
                userchannel.UserItems.Add(new UserItem(userId, item.Id));
            }

            _rssDatabase.UserChannels.AddOrUpdate(userchannel);
            _rssDatabase.SaveChanges();
            return userchannel.Id;
        }

        private void RestoreHiddenChannel(string userId, string url, UserChannel hiddenUserChannel)
        {
            hiddenUserChannel.IsHidden = false;

            var channelId = ReturnChannelId(url);
            IncreaseReadersCount(channelId);

            var userItems =
                _rssDatabase.UsersItems.Where(
                    foo => foo.ApplicationUserId == userId && foo.UserChannelId == hiddenUserChannel.Id).ToList();


            var userItemsIds = userItems.Select(foo => foo.ItemId).ToList();
            var items = _rssDatabase.AllItems.Where(x => x.ChannelId == channelId && !userItemsIds.Contains(x.Id)).ToList();

            foreach (var item in items)
            {
                hiddenUserChannel.UserItems.Add(new UserItem(userId, item.Id));
            }

            _rssDatabase.SaveChanges();
        }

        private DateTime GetDateTimeFromLastChannelItem(long channelId)
        {
            if (_rssDatabase.AllItems.Where(x => x.ChannelId == channelId).Any())
            {
                var lastItemDateTime = _rssDatabase.AllItems
                    .Where(x => x.ChannelId == channelId)
                    .Max(x => x.PublishDate);

                if (lastItemDateTime != null)
                {
                    return lastItemDateTime;
                }
            }
            return new DateTime(0);
        }

        private DateTime GetDateTimeFromLastUserChannelItem(long userChannelId)
        {
            if (_rssDatabase.UsersItems.Where(x => x.UserChannelId == userChannelId).Any())
            {
                var lastItemDateTime = _rssDatabase.UsersItems
                    .Where(x => x.UserChannelId == userChannelId)
                    .Max(x => x.Item.PublishDate);

                if (lastItemDateTime != null)
                {
                    return lastItemDateTime;
                }
            }

            return DateTime.Now;
        }
    }
}
