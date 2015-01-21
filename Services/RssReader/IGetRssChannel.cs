using System.ServiceModel.Syndication;
using Models.RSS;

namespace Services.RssReader
{
    public interface IGetRssChannel
    {
        SyndicationFeed GetRssChannelInfo(string url);
        Channel GetRssChannelWithFeeds(string url);
    }
}
