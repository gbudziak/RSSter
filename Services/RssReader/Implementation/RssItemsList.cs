using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Models.RSS;

namespace Services.RssReader.Implementation
{
    public class RssItemsList: IRssItemsList
    {

        public IEnumerable<Item> GetRssFeed(string blogUrl)
        {
            XDocument feedXml = XDocument.Load(blogUrl);
            var feeds = feedXml.Descendants("item").Select(feed => new Item
            {
                Title = feed.Element("title").Value,
                Link = feed.Element("link").Value,
                Description = Regex.Match(feed.Element("description").Value, @"^.{1,180}\b(?<!\s)").Value
            });

            return feeds;
        }
    }
}
