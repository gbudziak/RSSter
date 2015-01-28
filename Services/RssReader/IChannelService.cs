using System;
using System.Collections.Generic;
using Models.RSS;

namespace Services.RssReader
{
    public interface IChannelService
    {
        Channel GetChannelInfo(string userId, long userChannelId);

        long AddChannel(string userId, string url);

        void RemoveChannel(string userId, long channelId);

        long ReturnChannelId(string url);
        
        List<UserChannel> GetUserChannels(string userId);
        
        long ReturnUserChannelId(string url, string userId);

        void IncreaseReadersCount(long channelId);

        void DecreaseReadersCount(long channelId);

        void AddNewItemsToChannel(long userChannelId, string userId);

        void AddNewItemsToUserChannel(long userChannelId, string userId);

        List<long> GetUserChannelsIdList(string userId);

    }
}


