using System.ServiceModel.Syndication;
using System.Xml;

namespace Services.RssReader.Implementation
{
    public class SyndicationFeedProvider : ISyndicationFeedProvider
    {
        public SyndicationFeed GetSyndicationFeed(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            return feed;
        }
    }
}
