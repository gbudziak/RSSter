using System.Collections.Generic;
using Models.RSS;

namespace Services.RssReader
{
    public interface IItemService
    {
        List<UserItem> GetUserChannelItems(long channelId, string userId);

        List<UserItem> GetAllUserItems(string userId);

        List<UserItem> GetAllUnreadUserItems(string userId);

        void IncreaseUserRating(long userItemId);

        void DecreaseUserRating(long userItemId);

        void MarkAsRead(long userItemId);

        void MarkAllItemsAsRead(string userId);

        void MarkAllChannelItemsAsRead(string userId, long channelId);
        
        UserItem FetchUserItem(long userItemId);
    }
}