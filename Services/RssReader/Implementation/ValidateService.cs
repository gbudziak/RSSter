using System;
using System.Linq;
using RssDataContext;

namespace Services.RssReader.Implementation
{
    public class ValidateService : IValidateService
    {
        
        private readonly IApplicationRssDataContext _rssDatabase;
        private readonly ISyndicationFeedProvider _iSyndicationFeedProvider;


        public ValidateService(IApplicationRssDataContext rssDatabase, ISyndicationFeedProvider iSyndicationFeedProvider)
        {
            _rssDatabase = rssDatabase;
            _iSyndicationFeedProvider = iSyndicationFeedProvider;
        }

        public bool IsUrlUniqueInChannels(string url)
        {
            var linkCount = _rssDatabase.Channels.Where(foo => foo.Url == url).ToList();
            var result = !linkCount.Any();
            return result;
        }

        public bool IsUrlUniqueInUserChannels(string userId, string url)
        {
            var isInDatabase = _rssDatabase.Channels.FirstOrDefault(foo => foo.Url == url);
            if (isInDatabase == null)
            {
                return true;
            }
            var isInUserDatabase =
                _rssDatabase.UserChannels.Where(
                    foo => foo.ApplicationUserId == userId && foo.ChannelId == isInDatabase.Id && foo.IsHidden == false).ToList();
            var result = !isInUserDatabase.Any();
            return result;
        }

        public bool IsUrlValid(string url)
        {
            try
            {
                var urlValidation = _iSyndicationFeedProvider.GetSyndicationFeed(url);
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

        public bool AddChannelRemoteValidation(string url)
        {
            if (_rssDatabase.Channels.Any(x => x.Url == url))
            {
                var channelId = _rssDatabase.Channels.Single(x => x.Url == url).Id;
                return _rssDatabase.UserChannels.Where(x => x.ChannelId == channelId).ToList().Any();
            }
            
            return  false;
           
        }
        

    }
}
      