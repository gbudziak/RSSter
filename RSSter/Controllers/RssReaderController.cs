using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Linq;
using DBContext;
using Models.RSS;
using Services.RssReader;

//using Services.RssReader;

namespace RSSter.Controllers
{
    public class RssReaderController : Controller
    {
        private readonly IRssItemsList _rssItemsList;
        private readonly IChannelService _channelService;        

        public RssReaderController(IRssItemsList rssItemsList, IChannelService channelService)
        {
            _rssItemsList = rssItemsList;
            _channelService = channelService;
        }

        public ActionResult Index()
        {
            return View("Index");
        }


        public ActionResult RssListView(string link)
        {
            return View(_rssItemsList.GetRssFeed(link));
        }

        [HttpGet]
        public ActionResult AddRssChannel()
        {

            return View("AddRssChannel");
        }

        [HttpPost]
        public ActionResult AddRssChannel(Channel channel)
        {
            if (ModelState.IsValid)
            {
                var model =_rssItemsList.GetRssFeed(channel.Link);
                
                _channelService.AddChannel(1, model);
                return RedirectToAction("Index");
            }
            return View("AddRssChannel");
        }
        public ActionResult ChannelList()
        {
            //var model = new ChannelList();
            return View(TemporaryDb.TempDb);
        }
    }
}