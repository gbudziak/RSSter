using System.Collections.Generic;
using System.Linq;
using DBContext;
using Models.Models;
using Models.RSS;

namespace Services.RssReader.Implementation
{
    public class ChannelService : IChannelService
    {
        private readonly IApplicationDbContext _rssDatabase;

        public ChannelService(IApplicationDbContext rssDatabase)
        {
            _rssDatabase = rssDatabase;
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

        public void AddChannel(string userId, Channel newRssFeed)
        {
            if (!_rssDatabase.Channels.Any(foo => foo.Url == newRssFeed.Url))
            {
                _rssDatabase.Channels.Add(newRssFeed);
                _rssDatabase.SaveChanges();
            }

            var channelId = ReturnChannelId(newRssFeed.Url);
            var userchannel = new UserChannel(channelId, userId);
            var itemstosave =_rssDatabase.Channels.First(foo => foo.Url == newRssFeed.Url).Items.ToList();
            if (itemstosave.Count < 10)
            {
                foreach (var x in itemstosave)
                {
                    userchannel.UserItems.Add(new UserItem
                    {
                        ApplicationUser = new ApplicationUser(),
                        Id = channelId,
                        ApplicationUserId = userId,
                        Item = x,
                        ItemId = channelId,
                        RaitingMinus = x,
                        RaitingPlus = 0,
                        Read = 0
                    });

                }
            }
            _rssDatabase.UserChannels.Add(userchannel);
            
            _rssDatabase.SaveChanges();
        }

        public List<Channel> ShowChannelList()
        {
            return _rssDatabase.Channels.ToList();
        }

        public Channel ShowChannelFeedList(string url)
        {
            return _rssDatabase.Channels.First(x => x.Url == url);
        }

        public bool AddRaiting(string userId, long userChannelId, long userItemId)
        {
            var likeUp =
                _rssDatabase.UserChannels.First(foo => foo.ApplicationUserId == userId && foo.Id == userItemId)
                    .UserItems.First(goo => goo.Id == userItemId);
            return true;
        }

        public bool RemoveRaiting(string userId, long userChannelId, long userItemId)
        {
            return true;
        }

        public List<Channel> GetChannels(string userId)
        {
            var subscriptions = _rssDatabase.UserChannels.Where(x => x.ApplicationUser.Id == userId).ToList();

            var channels = new List<Channel>();

            foreach (var item in subscriptions)
            {
                channels.Add(_rssDatabase.Channels.First(x => x.Id == item.ChannelId));

            }
            return channels;
        }
    }
}
