using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.RSS;

namespace Services.RssReader.Implementation
{
    public class ChannelService : IChannelService
    {
        public void AddChannel(ChannelList list, string newRssFeed)
        {
            list.Channels.Add(new Channel(newRssFeed));
        }

        public void RemoveChannel(ChannelList list, string rssFeed)
        {
            var toRemove = list.Channels.FirstOrDefault(x => x.Link == rssFeed);
            list.Channels.Remove(toRemove);
        }
    }
}
