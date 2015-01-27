using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.RSS;
using RssDataContext;

namespace Services.RssReader.Implementation
{
    public class ItemService: IItemService
    {

         private readonly IApplicationRssDataContext _rssDatabase;
        private readonly IChannelGet _iChannelGet;

        public ItemService(IApplicationRssDataContext rssDatabase, IChannelGet iChannelGet)
        {
            _rssDatabase = rssDatabase;
            _iChannelGet = iChannelGet;
        }
        public List<UserItem> GetUserChannelItems(long userChannelId, string userId)
        {
            
            var items = _rssDatabase.UsersItems
                        .Where(x => x.UserChannelId == userChannelId)
                        .Where(x => x.ApplicationUserId == userId)
                        .ToList();

            return items;
        }

        public List<UserItem> GetAllUserItems(string userId)
        {
            var items = _rssDatabase.UsersItems
                        .Where(x => x.ApplicationUserId == userId)
                        .OrderByDescending(x => x.Item.PublishDate)
                        .ToList();

            return items;
        }

        public List<UserItem> GetAllUnreadUserItems(string userId)
        {
            var items = _rssDatabase.UsersItems
                        .Where(x => x.ApplicationUserId == userId)
                        .Where(x => x.Read == false)
                        .OrderByDescending(x => x.Item.PublishDate)
                        .ToList();

            return items;
        }
        
        public void AddRating(long userItemId)
        {
            var likeUp =
                _rssDatabase.UsersItems.First(foo => foo.Id == userItemId);
            var itemMaster =
                _rssDatabase.AllItems.First(foo => foo.Id == likeUp.ItemId);
            if (likeUp.RatingMinus)
            {
                likeUp.RatingMinus = false;
                itemMaster.RatingMinus--;
            }
            likeUp.RatingPlus = true;
            itemMaster.RatingPlus++;

            _rssDatabase.SaveChanges();
        }

        public void RemoveRating(long userItemId)
        {
            var likeDown =
                _rssDatabase.UsersItems.First(foo => foo.Id == userItemId);
            var itemMaster =
                _rssDatabase.AllItems.First(foo => foo.Id == likeDown.ItemId);
            if (likeDown.RatingPlus)
            {
                likeDown.RatingPlus = false;
                itemMaster.RatingPlus--;
            }
            likeDown.RatingMinus = true;
            itemMaster.RatingMinus++;

            _rssDatabase.SaveChanges();
        }

        public void MarkAsRead(long userItemId)
        {
            var userItem =
                _rssDatabase.UsersItems.Single(foo => foo.Id == userItemId);
            userItem.Read = true;

            _rssDatabase.SaveChanges();
        }

        public void MarkAllItemsAsRead(string userId)
        {
            var items = _rssDatabase.UsersItems
                        .Where(x => x.ApplicationUserId == userId)
                        .ToList();

            foreach (var item in items)
            {
                item.Read = true;
            }

            _rssDatabase.SaveChanges();
        }


        public void MarkAllChannelItemsAsRead(string userId, long channelId)
        {
            var items = _rssDatabase.UsersItems
                        .Where(x => x.UserChannelId == channelId)
                        .Where(x => x.ApplicationUserId == userId)
                        .ToList();

            foreach (var item in items)
            {
                item.Read = true;
            }

            _rssDatabase.SaveChanges();

        }
       
    }
}
