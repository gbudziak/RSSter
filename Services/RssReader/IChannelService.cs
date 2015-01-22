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

        bool AddRaiting(long userItemId);

        bool RemoveRaiting(long userItemId);

        List<UserChannel> GetChannels(string userId);
        bool MarkAsRead(string userId, long userChannelId, long userItemId);
    }
}


