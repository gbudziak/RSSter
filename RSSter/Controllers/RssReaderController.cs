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
using Microsoft.AspNet.Identity;
using Models.RSS;
using Services.RssReader;

namespace RSSter.Controllers
{
    public class RssReaderController : Controller
    {
        private readonly IDownloadChannelItemsList _downloadChannelItemsList;
        private readonly IChannelService _channelService;

        public RssReaderController(IDownloadChannelItemsList downloadChannelItemsList, IChannelService channelService)
        {
            _downloadChannelItemsList = downloadChannelItemsList;
            _channelService = channelService;
        }

        public ActionResult Index()
        {
            return View("Index");
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
                var model = _downloadChannelItemsList.GetRssChannelFeeds(channel.Link);
                var userId = User.Identity.GetUserId();
                
                _channelService.AddChannel(userId, model);
                return RedirectToAction("ChannelList");
            }
            return View("AddRssChannel");
        }

        public ActionResult RssListView(string link)
        {            
            return View(_channelService.ShowChannelFeedList(link));
        }

        public ActionResult ChannelList()
        {            
            return View(_channelService.ShowChannelList());
        }

        public ActionResult Delete(string link)
        {
            var userId = User.Identity.GetUserId();
            _channelService.RemoveChannel(userId, link);
            return RedirectToAction("ChannelList");
        }

        public ActionResult Channels()
        {
            var channels = _downloadChannelItemsList.GetChannels();
            return PartialView("Channels", channels);
        }
    }
}