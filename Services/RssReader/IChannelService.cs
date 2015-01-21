using Models.RSS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RssReader
{
    public interface IChannelService
    {
        void AddChannel(string userId, Channel newRssFeed);

        void RemoveChannel(string userId, string link);

        long ReturnChannelId(string link);

        List<Channel> ShowChannelList();

        Channel ShowChannelFeedList(string link);

        bool AddRaiting(string rssLink);

        bool RemoveRaiting(string rssLink);

    }
}


