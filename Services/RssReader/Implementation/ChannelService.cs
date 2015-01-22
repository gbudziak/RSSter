﻿using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DBContext;
using Models.RSS;

namespace Services.RssReader.Implementation
{
    public class ChannelService : IChannelService
    {
        private readonly IApplicationDbContext _rssDatabase;
        private readonly IGetRssChannel _iGetRssChannel;

        public ChannelService(IApplicationDbContext rssDatabase, IGetRssChannel iGetRssChannel)
        {
            _rssDatabase = rssDatabase;
            _iGetRssChannel = iGetRssChannel;
        }

        public void RemoveChannel(string userId, long userChannelId)
        {
            var toRemove =
                _rssDatabase.UserChannels.First(foo => foo.ApplicationUserId == userId && foo.Id == userChannelId);
            toRemove.IsHidden = true;
        }

        public long ReturnChannelId(string url)
        {
            var channelId = _rssDatabase.Channels.First(foo => foo.Url == url).Id;
            return channelId;
        }

        public void AddChannel(string userId, string url)
        {
            if (!_rssDatabase.Channels.Any(foo => foo.Url == url))
            {
                var model = _iGetRssChannel.GetRssChannelWithFeeds(url);
                _rssDatabase.Channels.Add(model);
                _rssDatabase.SaveChanges();
            }
            var channelId = ReturnChannelId(url);
            var userchannel = new UserChannel(channelId, userId);
            _rssDatabase.UserChannels.Add(userchannel);
            _rssDatabase.SaveChanges();

            var userChannelId =_rssDatabase.UserChannels.First(x => x.ChannelId == channelId && x.ApplicationUserId == userId).Id;
            var channels = _rssDatabase.Channels.First(x => x.Id == channelId);

            foreach (var item in channels.Items)
            {
                userchannel.UserItems.Add(new UserItem(userId, item.Id, userChannelId));
            }
            
            _rssDatabase.SaveChanges();
        }

        public List<Channel> ShowChannelList()
        {
            return _rssDatabase.Channels.ToList();
        }

        public List<UserItem> ShowChannelFeedList(long userChannelId, string userId)
        {
            return
                _rssDatabase.UsersItems.Where(x => x.UserChannelId == userChannelId && x.ApplicationUserId == userId)
                    .ToList();
        }

        public bool AddRaiting(string userId, long userChannelId, long userItemId)
        {
            var likeUp =
                _rssDatabase.UserChannels.First(foo => foo.ApplicationUserId == userId && foo.Id == userItemId)
                    .UserItems.First(goo => goo.Id == userItemId);
            var itemMaster =
                _rssDatabase.Channels.First(foo => foo.Id == userChannelId).Items.First(goo => goo.Id == likeUp.ItemId);
            if (likeUp.RaitingMinus)
            {
                likeUp.RaitingMinus = false;
                itemMaster.RaitingMinus--;
            }
            likeUp.RaitingPlus = true;
            itemMaster.RaitingPlus++;
            _rssDatabase.SaveChanges();
            return true;
        }

        public bool RemoveRaiting(string userId, long userChannelId, long userItemId)
        {
            var likeDown =
                _rssDatabase.UserChannels.First(foo => foo.ApplicationUserId == userId && foo.Id == userItemId)
                    .UserItems.First(goo => goo.Id == userItemId);
            var itemMaster =
                _rssDatabase.Channels.First(foo => foo.Id == userChannelId).Items.First(goo => goo.Id == likeDown.ItemId);
            if (likeDown.RaitingPlus)
            {
                likeDown.RaitingPlus = false;
                itemMaster.RaitingPlus--;
            }
            likeDown.RaitingMinus = true;
            itemMaster.RaitingMinus++;
            _rssDatabase.SaveChanges();
            return true;
        }


        public List<UserChannel> GetChannels(string userId)
        {
            var channels = _rssDatabase.UserChannels
                .Where(x => x.ApplicationUserId == userId && x.IsHidden == false).ToList();

            return channels;
        }
    }
}
