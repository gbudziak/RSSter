using System.Collections.Generic;
using Models.RSS;

namespace Services.RssReader
{
    public interface IChannelService
    {
        void AddChannel(string userId, string url);

        void RemoveChannel(string userId, long userChannelId);

        long ReturnChannelId(string url);

        List<Channel> ShowChannelList();

        Channel ShowChannelFeedList(string url);

        bool AddRaiting(string userId, long userChannelId, long userItemId);

        bool RemoveRaiting(string userId, long userChannelId, long userItemId);

        List<Channel> GetChannels();
    }
}


