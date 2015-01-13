using Models.RSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RssReader
{
    public interface IChannelService
    {
        void AddChannel(ChannelList list, string newRssFeed);

        void RemoveChannel(ChannelList list, string rssFeed);

    }
}


