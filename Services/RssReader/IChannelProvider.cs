using System;
using Models.RSS;

namespace Services.RssReader
{
    public interface IChannelProvider
    {
        Channel GetChannel(string url);
        Channel GetChannelWithNewItems(string url, DateTime lastItemDateTime,long channelId);
    }
}