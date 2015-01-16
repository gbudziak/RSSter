using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using Models.RSS;

namespace Services.RssReader
{
    public interface IDownloadChannelItemsList
    {
        //Channel GetRssFeed(string blogUrl);
        SyndicationFeed GetRssChannel(string blogUrl);
        Channel GetRssChannelInfo(string blogUrl);
        Channel GetRssChannelFeeds(string blogUrl);
        void RefreshChannelFeeds();



    }
}
