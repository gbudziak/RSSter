using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Models.RSS;
using RssDataContext;

namespace Services.RssReader.Implementation
{
    public class ChannelService : IChannelService
    {
        #region Constructor
        private readonly IApplicationRssDataContext _rssDatabase;
        private readonly IChannelGet _channelGet;

        public ChannelService(IApplicationRssDataContext rssDatabase, IChannelGet channelGet)
        {
            _rssDatabase = rssDatabase;
            _channelGet = channelGet;
        }
        #endregion
        public Channel GetChannelInfo(string userId, long userChannelId)
        {
            return _rssDatabase.UserChannels
                    .Where(userChannel => userChannel.ApplicationUserId == userId)
                    .FirstOrDefault(userChannel => userChannel.Id == userChannelId)
                    .Channel;
        }

        public long AddChannel(string userId, string url)
        {
            var result = -1L;
            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    CheckAndCreateChannel(url);
                    result = CreateOrRestoreUserChannel(userId, url, transaction);
                    transaction.Commit();
                    return result;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            return result;
        }


        public void RemoveChannel(string userId, long channelId)
        {
            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    var toRemove =
                        _rssDatabase.UserChannels
                            .Where(userChannel => userChannel.ApplicationUserId == userId)
                            .FirstOrDefault(userChannel => userChannel.ChannelId == channelId);
                    toRemove.IsHidden = true;
                    DecreaseReadersCount(channelId);
                    _rssDatabase.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        public List<UserChannel> GetUserChannels(string userId)
        {
            var channels = _rssDatabase.UserChannels
                            .Where(x => x.ApplicationUserId == userId)
                            .Where(x => x.IsHidden == false)
                            .ToList();

            return channels;
        }

        public long ReturnChannelId(string url)
        {
            var channelId = _rssDatabase.Channels.FirstOrDefault(channel => channel.Url == url).Id;
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
            _rssDatabase.Channels.FirstOrDefault(channel => channel.Id == channelId).Readers++;
        }

        public void DecreaseReadersCount(long channelId)
        {
            _rssDatabase.Channels.FirstOrDefault(channel => channel.Id == channelId).Readers--;
        }

        public void AddNewItemsToChannel(long userChannelId, string userId)
        {

            var mainChannelId = _rssDatabase.UserChannels
                .Where(x=>x.ApplicationUserId == userId)
                .Single(x=>x.Id == userChannelId).ChannelId;

            var lastItemDateTime = _rssDatabase.AllItems
                .Where(x => x.ChannelId == mainChannelId)
                .OrderByDescending(x=>x.PublishDate)
                .ToList()[0].PublishDate;


            var url = _rssDatabase.Channels
                .Single(x => x.Id == mainChannelId)
                .Url;

            var channel = _channelGet.GetUpdatedRssChannel(url, lastItemDateTime, mainChannelId);

            _rssDatabase.Channels.AddOrUpdate(channel);
            _rssDatabase.AllItems.AddRange(channel.Items);

            
            _rssDatabase.SaveChanges();

        }

        public void AddNewItemsToUserChannel(long userChannelId, string userId)
        {

            var mainChannelId = _rssDatabase.UserChannels
                .Where(x => x.ApplicationUserId == userId)
                .Single(x => x.Id == userChannelId).ChannelId;

           var lastdata = _rssDatabase.UsersItems.Where(x => x.UserChannelId == userChannelId)
                .OrderByDescending(x => x.Item.PublishDate).ToList()[0].Item.PublishDate;

            var items = _rssDatabase.AllItems
               .Where(x => x.ChannelId == mainChannelId)
               .Where(x => x.PublishDate > lastdata)
               .ToList();

            foreach (var item in items)
            {
               
                    var userItem = new UserItem(userId, item.Id) { UserChannelId = userChannelId };
                    _rssDatabase.UsersItems.Add(userItem);
                
            }

            _rssDatabase.SaveChanges();
        }

        #region PRIVATE METHODS

        private long CreateOrRestoreUserChannel(string userId, string url, IRssTransaction transaction)
        {
            var hiddenUserChannel =
                _rssDatabase.UserChannels
                    .Where(userChannel => userChannel.ApplicationUserId == userId)
                    .FirstOrDefault(userChannel => userChannel.Channel.Url == url);
            if (hiddenUserChannel != null)
            {
                RestoreHiddenChannel(userId, url, hiddenUserChannel);
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

        private void RefreshChannelInfoAndItems(string userId, string url, UserChannel hiddenUserChannel)
        {

        }

        #endregion

    }
}
