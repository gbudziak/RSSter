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

        List<UserItem> ShowChannelFeedList(long channelId, string userId);

        bool AddRaiting(string userId, long userChannelId, long userItemId);

        bool RemoveRaiting(string userId, long userChannelId, long userItemId);

        List<UserChannel> GetChannels(string userId);
    }
}


