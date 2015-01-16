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
        void AddChannel(Channel newRssFeed);

        void RemoveChannel(string link);

        List<Channel> ShowChannelList();
        Channel ShowChannelFeedList(string link);

    }
}


