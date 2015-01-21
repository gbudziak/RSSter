using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBContext;

namespace Services.RssReader.Implementation
{
    public class ValidateService : IValidateService
    {
        private readonly IApplicationDbContext _rssDatabase;

        public ValidateService(IApplicationDbContext rssDatabase)
        {
            _rssDatabase = rssDatabase;
        }
        
        public bool IsUrlUniqueInChannels(string url)
        {
            var linkCount = _rssDatabase.Channels.Where(foo => foo.Url == url).ToList();                  
            return !linkCount.Any();
        }

        public bool IsUrlUniqueInUserChannels(string userId, long channelId)
        {
            var linkCount =
                _rssDatabase.UserChannels.Where(foo => foo.ApplicationUserId == userId)
                    .Where(goo => goo.ChannelId == channelId)
                    .ToList();
            return !linkCount.Any();
        }

        public bool IsUrlValid(string url)
        {
            try
            {
                var urlValidation = _downloadChannelItemsList.GetRssChannel(url);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }        

        public bool IsUrlExist(string url)
        {
            var linkCount = _rssDatabase.Channels.Where(foo => foo.Url == url).ToList();
            return !linkCount.Any();
        }

    }
}
