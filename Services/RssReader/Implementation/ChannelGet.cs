using System;
using Models.RSS;

namespace Services.RssReader.Implementation
{


    public class ChannelGet : IChannelGet
    {
        private readonly IChannelProvider _channelProvider;

        public ChannelGet(IChannelProvider channelProvider)
        {
            _channelProvider = channelProvider;
        }

        public Channel GetRssChannelWithFeeds(string url)
        {
            return _channelProvider.GetChannel(url);
        }

        public Channel GetUpdatedRssChannel(string url, DateTime lastItemDateTime, long channelId)
        {
            return _channelProvider.GetChannelWithNewItems(url,lastItemDateTime,channelId);
        }
    }
}