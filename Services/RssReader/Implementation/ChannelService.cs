using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using RssDataContext;
using Models.RSS;

namespace Services.RssReader.Implementation
{
    public class ChannelService : IChannelService
    {
        #region Constructor
        private readonly IApplicationRssDataContext _rssDatabase;
        private readonly IChannelGet _iChannelGet;

        public ChannelService(IApplicationRssDataContext rssDatabase, IChannelGet iChannelGet)
        {
            _rssDatabase = rssDatabase;
            _iChannelGet = iChannelGet;
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
                .Id);
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
            var channel = _iChannelGet.GetRssChannelWithFeeds(url);
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

        #endregion

    }
}
