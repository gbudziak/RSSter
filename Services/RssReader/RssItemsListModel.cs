using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Models.RSS;

namespace Services.RssReader
{
    public class RssItemsListModel
    {
        private static string _blogURL = "http://www.tvn24.pl/najnowsze.xml";

        public static IEnumerable<Item> GetRssFeed()
        {
           XDocument feedXml = XDocument.Load(_blogURL);
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
