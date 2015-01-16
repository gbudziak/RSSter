using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBContext;
using Models.RSS;

namespace Services.RssReader.Implementation
{
    public class ChannelService : IChannelService
    {
        private readonly IDatabase _rssDatabase;

        public ChannelService(IDatabase rssDatabase)
        {
            _rssDatabase = rssDatabase;
        }


        public void RemoveChannel(int channelListId, long channelId)
        {
            var toRemove = _rssDatabase.Channels.FirstOrDefault(foo => foo.ChannelId == channelId);
            _rssDatabase.Channels.Remove(toRemove);
            
        }

        public void AddChannel(int channelListId, Channel newRssFeed)
        {
            _rssDatabase.Channels.Add(newRssFeed);
        }

        public List<Channel> ShowChannelList()
        {
            return _rssDatabase.Channels;
        }

        public Channel ShowChannelFeedList(string link)
        {
            return _rssDatabase.Channels.First(x => x.Link == link);
        }
    }
}
