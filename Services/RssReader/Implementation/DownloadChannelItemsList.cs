using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using DBContext;
using Models.RSS;
using Services.RssReader;






namespace Services.RssReader.Implementation
{
    public interface ISyndicateFeed
    {
        Channel GetChannel(string url);
    }

    public class DownloadChannelItemsList : IDownloadChannelItemsList
    {
        private readonly ISyndicateFeed _syndicateFeed;
        private readonly IApplicationDbContext _rssDatabase;

        public DownloadChannelItemsList(ISyndicateFeed syndicateFeed, IApplicationDbContext rssDatabase)
        {
            _syndicateFeed = syndicateFeed;
            _rssDatabase = rssDatabase;
        }

        public SyndicationFeed GetRssChannel(string blogUrl)
        {
            throw new NotImplementedException();
        }

        public Channel GetRssChannelFeeds(string blogUrl)
        {
            _syndicateFeed.GetChannel(blogUrl);
        }

        public List<Channel> GetChannels()
        {
            var channels = _rssDatabase.Channels.ToList();
            return channels;
        }

    }
}