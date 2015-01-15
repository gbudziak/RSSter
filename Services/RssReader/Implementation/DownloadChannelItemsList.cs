using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Models.RSS;


namespace Services.RssReader.Implementation
{
    public class downloadChannelItemsList: IDownloadChannelItemsList
    {

        public Channel GetRssFeed(string blogUrl)
        {
            XmlReader reader = XmlReader.Create(blogUrl);

            var feed = SyndicationFeed.Load(reader);

            var channelModel = new Channel{Link = blogUrl};
            List<Item> itemList = new List<Item>();

            if (feed != null)
            {
                //var channelDescription = feed.Description.Text;
                //var channelImage = feed.ImageUrl;

                foreach (SyndicationItem item in feed.Items)
                {
                    //var itemId = item.Id;
                    //var itemPublishDate = item.PublishDate;
                    //var itemLink = item.Links[0].Uri;
                    //var itemSummary = item.Summary.Text;
                    //var itemTitle = item.Title.Text;
                    //var itemCategory = item.Categories[0].Name;

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
            channelModel.Description = feed.Description.Text;
            channelModel.Image = feed.ImageUrl.ToString();
            
            return channelModel;
        }
    }
}



//XDocument feedXml = XDocument.Load(blogUrl);
//var feeds = feedXml.Descendants("item").Select(feed => new Item
//{
//    Title = feed.Element("title").Value,
//    Link = feed.Element("link").Value,
//    Description = Regex.Match(feed.Element("description").Value, @"^.{1,180}\b(?<!\s)").Value
//});