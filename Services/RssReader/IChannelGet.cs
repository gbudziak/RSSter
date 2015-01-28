using System;
using Models.RSS;

namespace Services.RssReader
{
    public interface IChannelGet
    {
        Channel GetRssChannelWithFeeds(string url);
        Channel GetUpdatedRssChannel(string url, DateTime lastItemDateTime, long channelId);
    }
}
