using System.Collections.Generic;

using Models.ViewModels;
using Models.RSS;
using System;

namespace Services.RssReader
{
    public interface IChannelService
    {
        long AddChannel(string userId, string url);

        void AddChannel(string url);

        void RemoveChannel(string userId, long userChannelId);

        long ReturnChannelId(string url);
        
        List<ShowUserChannelsViewModel> GetUserChannels(string userId);
        
        long ReturnUserChannelId(string url, string userId);

        void IncreaseReadersCount(long channelId);

        void DecreaseReadersCount(long channelId);

        void AddNewItemsToChannel(long channelId, string url);

        void AddNewItemsToUserChannel(string userId, long channelId, long userChannelId);

        //void AddToHistory(string actionName, DateTime date, long userChannelId, long userItemId, string subscriptionId, string userId);

        //List<long> GetUserChannelsIdList(string userId);

    }
}


