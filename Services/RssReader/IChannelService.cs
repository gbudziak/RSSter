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
        void AddChannel(Channel newRssFeed);

        void RemoveChannel(string link);

        DbSet<Channel> ShowChannelList();

        Channel ShowChannelFeedList(string link);

        bool AddRaiting(string rssLink);

        bool RemoveRaiting(string rssLink);

    }
}


