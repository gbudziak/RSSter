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
            return
                _rssDatabase.UsersItems.Where(x => x.UserChannelId == userChannelId && x.ApplicationUserId == userId && x.Read == false)
                    .ToList();
        }

        public List<UserChannel> GetUserChannels(string userId)
        {
            var channels = _rssDatabase.UserChannels
                .Where(x => x.ApplicationUserId == userId && x.IsHidden == false).ToList();

            return channels;
        }

        public void RemoveChannel(string userId, long userChannelId)
        {
            var toRemove =
                _rssDatabase.UserChannels.First(foo => foo.ApplicationUserId == userId && foo.Id == userChannelId);
            toRemove.IsHidden = true;
        }

        public long ReturnChannelId(string url)
        {
            var channelId = _rssDatabase.Channels.First(foo => foo.Url == url).Id;
            return channelId;
        }


        public void AddChannel(string userId, string url)
        {
            if (!_rssDatabase.Channels.Any(foo => foo.Url == url))
            {
                var model = _iGetRssChannel.GetRssChannelWithFeeds(url);
                _rssDatabase.Channels.Add(model);
                _rssDatabase.SaveChanges();
            }

            var channelId = ReturnChannelId(url);
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
                _rssDatabase.UsersItems.First(foo => foo.Id == userItemId);
            userItem.Read = true;
        }
    }
}
