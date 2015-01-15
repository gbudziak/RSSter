using System;
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
            TemporaryDb.TempDb.Channels.Remove(toRemove);
            
        }

        public void AddChannel(int channelListId, Channel newRssFeed)
        {            
            TemporaryDb.TempDb.Channels.Add(newRssFeed);
            
        }

        public ChannelList ShowChannelList()
        {
            return TemporaryDb.TempDb;
        }
    }
}
