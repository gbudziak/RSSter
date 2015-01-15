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
        public void RemoveChannel(ChannelList list, string rssFeed)
        {
            var toRemove = list.Channels.FirstOrDefault(x => x.Link == rssFeed);
            list.Channels.Remove(toRemove);
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
