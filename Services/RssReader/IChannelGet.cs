using System.ServiceModel.Syndication;
using Models.RSS;

namespace Services.RssReader
{
    public interface IChannelGet
    {
        SyndicationFeed GetRssChannelInfo(string url);
        Channel GetRssChannelWithFeeds(string url);
    }
}
