using System;
using System.ServiceModel.Syndication;
using Models.RSS;

namespace Services.RssReader.Implementation
{


    public class ChannelGet : IChannelGet
    {
        private readonly IChannelProvider _syndicateFeed;

        public ChannelGet(IChannelProvider syndicateFeed)
        {
            _syndicateFeed = syndicateFeed;
        }

        public SyndicationFeed GetRssChannelInfo(string url)
        {
            //TO DO
            throw new NotImplementedException();
        }

        public Channel GetRssChannelWithFeeds(string url)
        {
           return _syndicateFeed.GetChannel(url);
        }



    }
}