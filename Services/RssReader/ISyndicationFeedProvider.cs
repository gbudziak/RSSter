using System.ServiceModel.Syndication;

namespace Services.RssReader
{
    public interface ISyndicationFeedProvider
    {
        SyndicationFeed GetSyndicationFeed(string url);
    }
}