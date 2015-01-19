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

        /// <summary>
        /// Method that removes channel with given url from channel list.
        /// </summary>
        /// <param name="link">Rss channel URL</param>
        public void RemoveChannel(string link)
        {
            var toRemove = _rssDatabase.Channels.FirstOrDefault(foo => foo.Link == link);
            _rssDatabase.Channels.Remove(toRemove);
            
        }

        /// <summary>
        /// Method that adds channel with given url to the channel list
        /// </summary>
        /// <param name="newRssFeed">Rss channel URL</param>
        public void AddChannel(Channel newRssFeed)
        {
            _rssDatabase.Channels.Add(newRssFeed);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Channel> ShowChannelList()
        {
            return _rssDatabase.Channels;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public Channel ShowChannelFeedList(string link)
        {
            return _rssDatabase.Channels.First(x => x.Link == link);
        }

        /// <summary>
        /// Method to add raiting to channel.
        /// </summary>
        /// <param name="link">Rss channel URL</param>
        /// <returns>bool, true if successful</returns>
        public bool AddRaiting(string link)
        {
            try
            {
                var channel = _rssDatabase.Channels.First(foo => foo.Link == link);
                channel.Raiting++;
                return true;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }

        /// <summary>
        /// Method to remove raiting from a channel
        /// </summary>
        /// <param name="link">Rss channel URL</param>
        /// <returns>bool, true if successful</returns>
        public bool RemoveRaiting(string link)
        {
            try
            {
                var channel = _rssDatabase.Channels.First(foo => foo.Link == link);
                channel.Raiting--;
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
