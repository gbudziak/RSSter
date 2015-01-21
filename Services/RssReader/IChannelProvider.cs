using Models.RSS;

namespace Services.RssReader
{
    public interface IChannelProvider
    {
        Channel GetChannel(string url);
    }
}
