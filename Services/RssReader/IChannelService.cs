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
        void AddChannel(int channelListId, Channel newRssFeed);

        void RemoveChannel(int channelListId, long channelId);

        ChannelList ShowChannelList();
    }
}


