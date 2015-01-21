using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBContext;
using Models.RSS;
using Services.RssReader.Implementation;

namespace Services.RssReader.Implementation
{
    public class ChannelService : IChannelService
    {
        private readonly IApplicationDbContext _rssDatabase;
        private readonly IValidateService _validateService;

        public ChannelService(IApplicationDbContext rssDatabase, IValidateService validateService)
        {
            _rssDatabase = rssDatabase;
            _validateService = validateService;
        }
    
        public void RemoveChannel(string userId, string link)
        {
            var toRemove = _rssDatabase.Channels.FirstOrDefault(foo => foo.Link == link);
            _rssDatabase.Channels.Remove(toRemove);
            
        }

        public long ReturnChannelId(string link)
        {
            var channelId = _rssDatabase.Channels.First(foo => foo.Link == link).Id;
            return channelId;
        }

        public void AddChannel(string userId, Channel newRssFeed)
        {            
            if (_validateService.IsLinkUniqueInChannels(newRssFeed.Link))
            {
                _rssDatabase.Channels.Add(newRssFeed);    
                _rssDatabase.SaveChanges();
            }
            var channelId = ReturnChannelId(newRssFeed.Link);
            if (_validateService.IsLinkUniqueInUserChannels(userId, channelId))
            {
                var userchannel = new UserChannel(channelId,userId);
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
