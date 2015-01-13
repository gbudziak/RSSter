using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Models.RSS;

namespace RSSter.Controllers
{
    public class RssReaderController : Controller
    {

        // GET: RssReader
        public ActionResult RssListView()
        {


            const string _blogURL = "http://www.tvn24.pl/najnowsze.xml";
            XDocument feedXml = XDocument.Load(_blogURL);
            var feeds = from feed in feedXml.Descendants("item")
               select new Item
               {
                   Title = feed.Element("title").Value,
                   Link = feed.Element("link").Value,
                   Description = Regex.Match(feed.Element("description").Value, @"^.{1,180}\b(?<!\s)").Value
 
               };
 
            return View();
        }
    }
}