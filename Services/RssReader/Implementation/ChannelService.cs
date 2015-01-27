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
        private readonly IApplicationRssDataContext _rssDatabase;
        private readonly IChannelGet _iChannelGet;

        public ChannelService(IApplicationRssDataContext rssDatabase, IChannelGet iChannelGet)
        {
            _rssDatabase = rssDatabase;
            _iChannelGet = iChannelGet;
        }

        public Channel GetChannelInfo(string userId, long userChannelId)
        {
            return _rssDatabase.UserChannels.First(foo => foo.ApplicationUserId == userId && foo.Id == userChannelId)
                    .Channel;
        }

        public long AddChannel(string userId, string url)
        {
            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    if (!_rssDatabase.Channels.Any(foo => foo.Url == url))
                    {
                        var model = _iChannelGet.GetRssChannelWithFeeds(url);
                        _rssDatabase.Channels.Add(model);
                        _rssDatabase.SaveChanges();
                    }
                    var hiddenUserChannel =
                        _rssDatabase.UserChannels.FirstOrDefault(
                            foo => foo.ApplicationUserId == userId && foo.Channel.Url == url);
                    if (hiddenUserChannel != null)
                    {
                        RestoreHiddenChannel(userId, url, hiddenUserChannel);
                        transaction.Commit();
                        return hiddenUserChannel.Id;
                    }
                    else
                    {
                        var userChannelId = CreateNewUserChannel(userId, url);
                        transaction.Commit();
                        return userChannelId;
                    }

                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            return 1; //TO IMPROVE
        }


        public void RemoveChannel(string userId, long channelId)
        {
            var toRemove =
                _rssDatabase.UserChannels.First(foo => foo.ApplicationUserId == userId && foo.ChannelId == channelId);
            toRemove.IsHidden = true;
            RemoveReader(channelId);
            _rssDatabase.SaveChanges();
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
            var channelId = _rssDatabase.Channels.First(foo => foo.Url == url).Id;
            return channelId;
        }

        public long ReturnUserChannelId(string url, string userId)
        {
            var userChannelId =
                _rssDatabase.UserChannels.First(foo => foo.ApplicationUserId == userId && foo.Channel.Url == url).Id;
            return userChannelId;
        }

        public void AddReader(long channelId)
        {
            _rssDatabase.Channels.First(foo => foo.Id == channelId).Readers++;
        }

        public void RemoveReader(long channelId)
        {
            _rssDatabase.Channels.First(foo => foo.Id == channelId).Readers--;
        }



        #region PRIVATE METHODS

        private long CreateNewUserChannel(string userId, string url)
        {
            var channelId = ReturnChannelId(url);
            AddReader(channelId);

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
            AddReader(channelId);
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
