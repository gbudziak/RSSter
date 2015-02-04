using System;
using System.Collections.Generic;
using Models.RSS;
using Models.ViewModels;

namespace Services.RssReader
{
    public interface IItemService
    {
        Channel GetChannelWithItems(long channelId);

        UserItemsViewModel GetUserChannelItems(long channelId, string userId, int viewType, int page, int pageSize);

        List<ShowAllUserItemsViewModel> GetAllUserItems(string userId);

        List<ShowAllUserItemsViewModel> GetAllUnreadUserItems(string userId);

        bool IncreaseUserRating(long userItemId);

        bool DecreaseUserRating(long userItemId);

        void MarkAsRead(long userItemId);

        void MarkAllItemsAsRead(string userId);

        void MarkAllChannelItemsAsRead(string userId, long channelId);
        
        string CalculateItemAge(DateTime publishTime);
    }
}