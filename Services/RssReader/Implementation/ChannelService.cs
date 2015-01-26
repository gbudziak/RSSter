using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Linq;
using DBContext;
using Models.RSS;

namespace Services.RssReader.Implementation
{
    public class ChannelService : IChannelService
    {
        private readonly IApplicationDbContext _rssDatabase;
        private readonly IGetRssChannel _iGetRssChannel;

        public ChannelService(IApplicationDbContext rssDatabase, IGetRssChannel iGetRssChannel)
        {
            _rssDatabase = rssDatabase;
            _iGetRssChannel = iGetRssChannel;
        }

        public List<UserItem> GetUserItems(long userChannelId, string userId)
        {
            //return
            //    _rssDatabase.UsersItems.Where(x => x.UserChannelId == userChannelId && x.ApplicationUserId == userId && x.Read == false)
            //        .ToList();
            return
    _rssDatabase.UsersItems.Where(x => x.UserChannelId == userChannelId && x.ApplicationUserId == userId)
        .ToList();
        }

        public List<UserChannel> GetUserChannels(string userId)
        {
            var channels = _rssDatabase.UserChannels
                .Where(x => x.ApplicationUserId == userId && x.IsHidden == false).ToList();

            return channels;
        }

        public List<UserItem> GetAllUserItems(string userId)
        {
            var items = _rssDatabase.UsersItems.Where(x => x.ApplicationUserId == userId).OrderByDescending(x=>x.Item.PublishDate).ToList();

            return items;
        }

        public List<UserItem> GetAllUnreadUserItems(string userId)
        {
            var items = _rssDatabase.UsersItems.Where(x => x.ApplicationUserId == userId).Where(x=>x.Read == false).OrderByDescending(x=>x.Item.PublishDate).ToList();

            return items;
        }


        public void RemoveChannel(string userId, long channelId)
        {
            var toRemove =
                _rssDatabase.UserChannels.First(foo => foo.ApplicationUserId == userId && foo.ChannelId == channelId);
            toRemove.IsHidden = true;
            RemoveReader(channelId);
            _rssDatabase.SaveChanges();
        }

        public long ReturnChannelId(string url)
        {
            var channelId = _rssDatabase.Channels.First(foo => foo.Url == url).Id;
            return channelId;
        }

        public long ReturnUserChannelId(string url, string userId)
        {
            var userChannelId =
                _rssDatabase.UserChannels.First(foo => foo.ApplicationUserId == userId && foo.Channel.Url == url).Id;
            return userChannelId;
        }

        public void AddReader(long channelId)
        {
            _rssDatabase.Channels.First(foo => foo.Id == channelId).Readers++;
        }

        public void RemoveReader(long channelId)
        {
            _rssDatabase.Channels.First(foo => foo.Id == channelId).Readers--;
        }


        public void AddChannel(string userId, string url)
        {
            if (!_rssDatabase.Channels.Any(foo => foo.Url == url))
            {
                var model = _iGetRssChannel.GetRssChannelWithFeeds(url);
                _rssDatabase.Channels.Add(model);
                _rssDatabase.SaveChanges();
            }
            var hiddenUserChannel =
                _rssDatabase.UserChannels.FirstOrDefault(
                    foo => foo.ApplicationUserId == userId && foo.Channel.Url == url);
            if (hiddenUserChannel != null)
            {
                RestoreHiddenChannel(userId, url, hiddenUserChannel);
            }
            else
            {
                CreateNewUserChannel(userId, url);
            }
            
        }

        private void RestoreHiddenChannel(string userId, string url, UserChannel hiddenUserChannel)
        {
            hiddenUserChannel.IsHidden = false;
            var channelId = ReturnChannelId(url);
            AddReader(channelId);
            var userItems =
                _rssDatabase.UsersItems.Where(
                    foo => foo.ApplicationUserId == userId && foo.UserChannelId == hiddenUserChannel.Id).ToList();
            var userItemsIds = userItems.Select(foo => foo.ItemId).ToList();
            var items = _rssDatabase.AllItems.Where(x => x.ChannelId == channelId && !userItemsIds.Contains(x.Id)).ToList();
            foreach (var item in items)
            {
                hiddenUserChannel.UserItems.Add(new UserItem(userId, item.Id));
            }
            _rssDatabase.SaveChanges();
        }

        private void CreateNewUserChannel(string userId, string url)
        {
            var channelId = ReturnChannelId(url);
            AddReader(channelId);
            var userchannel = new UserChannel(channelId, userId);
            var channel = _rssDatabase.Channels.Single(x => x.Id == channelId);
            var items = _rssDatabase.AllItems.Where(x => x.ChannelId == channelId).ToList();
            foreach (var item in items)
            {
                userchannel.UserItems.Add(new UserItem(userId, item.Id));
            }
            _rssDatabase.UserChannels.AddOrUpdate(userchannel);
            _rssDatabase.SaveChanges();
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

        public Channel GetChannelInfo(string userId, long userChannelId)
        {
            return _rssDatabase.UserChannels.First(foo => foo.ApplicationUserId == userId && foo.Id == userChannelId)
                    .Channel;            
        }

        public void MarkAllItemsAsRead(string userId,long channelId)
        {
            if (channelId == 0)
            {
                var items = _rssDatabase.UsersItems.Where(
                    x=>x.ApplicationUserId == userId)
                    .ToList();
                foreach (var item in items)
                    item.Read = true;

                _rssDatabase.SaveChanges();
            }
            else
            {
                var items = _rssDatabase.UsersItems.Where(
                    x => x.UserChannelId == channelId && x.ApplicationUserId == userId)
                    .ToList();
                foreach (var item in items)
                    item.Read = true;

                _rssDatabase.SaveChanges();
            }

           

        }
    }
}
