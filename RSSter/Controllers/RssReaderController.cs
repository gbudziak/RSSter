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
        private readonly IGetRssChannel _downloadChannelItemsList;
        private readonly IChannelService _channelService;

        public RssReaderController(IGetRssChannel downloadChannelItemsList, IChannelService channelService)
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
                var model = _downloadChannelItemsList.GetRssChannelFeeds(channel.Url);
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

        public ActionResult Delete(long userChannelId)
        {
            var userId = User.Identity.GetUserId();
            _channelService.RemoveChannel(userId, userChannelId);
            return RedirectToAction("ChannelList");
        }

        public ActionResult Channels()
        {
            var channels = _downloadChannelItemsList.GetChannels();
            return PartialView("Channels", channels);
        }

        public JsonResult RaitingUp(long userChannelId, long userItemId)
        {
            var userId = User.Identity.GetUserId();
            var result = _channelService.AddRaiting(userId, userChannelId, userItemId);
            return Json(result);
        }

        public JsonResult RaitingDown(long userChannelId, long userItemId)
        {
            var userId = User.Identity.GetUserId();
            var result = _channelService.RemoveRaiting(userId, userChannelId, userItemId);
            return Json(result);
        }
    }
}