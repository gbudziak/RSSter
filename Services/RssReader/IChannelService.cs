using System.Collections.Generic;
using Models.RSS;

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

        List<Channel> GetChannels();
    }
}


