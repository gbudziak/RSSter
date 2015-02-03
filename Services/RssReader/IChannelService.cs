using System;
using System.Collections.Generic;
using Models.RSS;
using Models.ViewModels;

namespace Services.RssReader
{
    public interface IChannelService
    {
        long AddChannel(string userId, string url);

        void RemoveChannel(string userId, long channelId);

        long ReturnChannelId(string url);
        
        List<ShowUserChannelsViewModel> GetUserChannels(string userId);
        
        long ReturnUserChannelId(string url, string userId);

        void IncreaseReadersCount(long channelId);

        void DecreaseReadersCount(long channelId);

        void AddNewItemsToChannel(long channelId, string url);

        void AddNewItemsToUserChannel(string userId, long channelId, long userChannelId);

        //List<long> GetUserChannelsIdList(string userId);

    }
}


