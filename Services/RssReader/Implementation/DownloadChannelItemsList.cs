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
    public class DownloadChannelItemsList: IDownloadChannelItemsList
    {
        private readonly IDatabase _rssDatabase;
        private readonly IChannelService _channelService;

        public DownloadChannelItemsList(IDatabase rssDatabase, IChannelService channelService)
        {
            _rssDatabase = rssDatabase;
            _channelService = channelService;
        }

        public SyndicationFeed GetRssChannel(string blogUrl)
        {
            XmlReader reader = XmlReader.Create(blogUrl);

            var feed = SyndicationFeed.Load(reader);

            return feed;
        }

        public Channel GetRssChannelInfo(string blogUrl)
        {
            var feed = GetRssChannel(blogUrl);

            var channelModel = new Channel
            {
                Link = blogUrl,
                Description = feed.Description.Text,
                Title = feed.Title.Text,
                Image = feed.ImageUrl.ToString()

            };

            return channelModel;

        }
        public Channel GetRssChannelFeeds(string blogUrl)
        {
            var feed = GetRssChannel(blogUrl);
            var channelModel = new Channel
            {
                Link = blogUrl,
                Title = feed.Title.Text,
                Description = feed.Description.Text,
                Image = feed.ImageUrl.ToString()
            };
            var itemList = new List<Item>();

            if (feed != null)
            {
                foreach (SyndicationItem item in feed.Items)
                {
                    itemList.Add(new Item
                    {
                        Description = item.Summary.Text,
                        Link = item.Links[0].Uri.ToString(),
                        Title = item.Title.Text,
                        PublishDate = item.PublishDate.ToString(),
                        Category = "none"
                    });
                }
            }
            channelModel.Items = itemList;
            return channelModel;
        }

 
        //public void RefreshChannelFeeds()
        //{
        //    var channelLinkList = _rssDatabase.Channels.Select(x => x.Link);
        //    foreach (var x in channelLinkList)
        //    {
        //        var model = GetRssChannelFeeds(x);
                
        //        _channelService.AddChannel(1,model);
        //    }
        //}

        
    }
}



//XDocument feedXml = XDocument.Load(blogUrl);
//var feeds = feedXml.Descendants("item").Select(feed => new Item
//{
//    Title = feed.Element("title").Value,
//    Link = feed.Element("link").Value,
//    Description = Regex.Match(feed.Element("description").Value, @"^.{1,180}\b(?<!\s)").Value
//});


//var itemId = item.Id;
//var itemPublishDate = item.PublishDate;
//var itemLink = item.Links[0].Uri;
//var itemSummary = item.Summary.Text;
//var itemTitle = item.Title.Text;
//var itemCategory = item.Categories[0].Name;