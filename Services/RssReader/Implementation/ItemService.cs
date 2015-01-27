using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.RSS;
using RssDataContext;

namespace Services.RssReader.Implementation
{
    public class ItemService : IItemService
    {
        #region Constructor
        private readonly IApplicationRssDataContext _rssDatabase;
        private readonly IChannelGet _iChannelGet;

        public ItemService(IApplicationRssDataContext rssDatabase, IChannelGet iChannelGet)
        {
            _rssDatabase = rssDatabase;
            _iChannelGet = iChannelGet;
        }
        #endregion
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

        public void IncreaseUserRating(long userItemId)
        {
            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    IncreaseItemRating(userItemId);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }



        public void DecreaseUserRating(long userItemId)
        {
            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    DecreaseItemRating(userItemId);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }



        public void MarkAsRead(long userItemId)
        {
            using (var transaction = _rssDatabase.OpenTransaction())
            {
                try
                {
                    var userItem =
                        _rssDatabase.UsersItems
                            .Single(item => item.Id == userItemId);
                    userItem.Read = true;

                    _rssDatabase.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
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

        #region Private Methods
        private void IncreaseItemRating(long userItemId)
        {
            var userItem =
                _rssDatabase.UsersItems
                    .FirstOrDefault(querry => querry.Id == userItemId);
            var item =
                _rssDatabase.AllItems
                    .FirstOrDefault(querry => querry.Id == userItem.ItemId);
            if (userItem.RatingMinus)
            {
                userItem.RatingMinus = false;
                item.RatingMinus--;
            }
            userItem.RatingPlus = true;
            item.RatingPlus++;

            _rssDatabase.SaveChanges();
        }

        private void DecreaseItemRating(long userItemId)
        {
            var userItem =
                _rssDatabase.UsersItems.FirstOrDefault(querry => querry.Id == userItemId);
            var item =
                _rssDatabase.AllItems.FirstOrDefault(querry => querry.Id == userItem.ItemId);
            if (userItem.RatingPlus)
            {
                userItem.RatingPlus = false;
                item.RatingPlus--;
            }
            userItem.RatingMinus = true;
            item.RatingMinus++;

            _rssDatabase.SaveChanges();
        }
        #endregion

    }
}
