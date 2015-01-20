using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBContext;
using Models.RSS;

namespace Services.RssReader.Implementation
{
    public class ChannelService : IChannelService
    {
        private readonly IApplicationDbContext _rssDatabase;

        public ChannelService(IApplicationDbContext rssDatabase)
        {
            _rssDatabase = rssDatabase;
        }
    
        public void RemoveChannel(string userId, string link)
        {
            var toRemove = _rssDatabase.Channels.FirstOrDefault(foo => foo.Link == link);
            _rssDatabase.Channels.Remove(toRemove);
            
        }
     
        public void AddChannel(string userId, Channel newRssFeed)
        {
            var linkCount = _rssDatabase.Channels.Where(foo => foo.Link == newRssFeed.Link).ToList();
            if (!linkCount.Any())
            {
                _rssDatabase.Channels.Add(newRssFeed);    
                _rssDatabase.SaveChanges();
            }
            var channelId = _rssDatabase.Channels.FirstOrDefault(foo => foo.Link == newRssFeed.Link).Id;
            var linkCount02 = _rssDatabase.UserChannels.Where(foo => foo.ChannelId == channelId).ToList();
            if (!linkCount02.Any())
            {
                var userchannel = new UserChannel() {ApplicationUserId = userId, ChannelId = channelId};
                _rssDatabase.UserChannels.Add(userchannel);
                _rssDatabase.SaveChanges();
            }
        }
      
        public List<Channel> ShowChannelList()
        {
            return _rssDatabase.Channels.ToList();
        }
       
        public Channel ShowChannelFeedList(string link)
        {
            return _rssDatabase.Channels.First(x => x.Link == link);
        }
    
        public bool AddRaiting(string link)
        {
            try
            {
                var channel = _rssDatabase.Channels.First(foo => foo.Link == link);
                //channel.Raiting++;
                return true;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }
      
        public bool RemoveRaiting(string link)
        {
            try
            {
                var channel = _rssDatabase.Channels.First(foo => foo.Link == link);
                //channel.Raiting--;
                return true;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
