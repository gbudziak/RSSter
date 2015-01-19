using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBContext;
using Models.RSS;

namespace Services.RssReader.Implementation
{
    public class ChannelService : IChannelService
    {
        private readonly IDatabase _rssDatabase;

        public ChannelService(IDatabase rssDatabase)
        {
            _rssDatabase = rssDatabase;
        }
    
        public void RemoveChannel(string link)
        {
            var toRemove = _rssDatabase.Channels.FirstOrDefault(foo => foo.Link == link);
            _rssDatabase.Channels.Remove(toRemove);
            
        }
     
        public void AddChannel(Channel newRssFeed)
        {
            _rssDatabase.Channels.Add(newRssFeed);
        }
      
        public List<Channel> ShowChannelList()
        {
            return _rssDatabase.Channels;
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
