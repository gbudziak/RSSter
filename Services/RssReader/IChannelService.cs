using System.Collections.Generic;
using Models.RSS;

namespace Services.RssReader
{
    public interface IChannelService
    {
        void AddChannel(string userId, string url);

        void RemoveChannel(string userId, long channelId);

        long ReturnChannelId(string url);

        List<UserItem> GetUserItems(long channelId, string userId);

        void AddRating(long userItemId);

        void RemoveRating(long userItemId);

        List<UserChannel> GetUserChannels(string userId);

        void MarkAsRead(long userItemId);
        
        Channel GetChannelInfo(string userId, long userChannelId);
    }
}


