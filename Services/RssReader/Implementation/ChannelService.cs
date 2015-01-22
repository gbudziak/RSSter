using System.Collections.Generic;
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

   
        public List<Channel> GetChannels()
        {
            var channels = _rssDatabase.Channels.ToList();
            return channels;
        }
    }
}
