using System;
using System.Collections.Generic;
using Models.RSS;
using Models.User;
using Models.ViewModels;

namespace Services.RssReader
{
    public interface IItemService
    {
        Channel GetChannelWithItems(long channelId);

        UserItemsViewModel GetUserChannelItems(long channelId, string userId, UserViewType viewType, int page, int pageSize);

        List<ShowAllUserItemsViewModel> GetAllUserItems(string userId);

        List<ShowAllUserItemsViewModel> GetAllUnreadUserItems(string userId);

        bool IncreaseUserRating(long userItemId, string userId);

        bool DecreaseUserRating(long userItemId, string userId);

        void MarkAsRead(long userItemId, string userId);

        void MarkAllItemsAsRead(string userId);

        void MarkAllChannelItemsAsRead(string userId, long channelId);
        
        string CalculateItemAge(DateTime publishTime);
        
        CompleteItemInfo GetSampleCompleteItemInfo();
    }
}