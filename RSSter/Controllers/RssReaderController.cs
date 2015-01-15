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
                
                _channelService.AddChannel(1, model);
                return RedirectToAction("Index");
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

        public ActionResult Delete(long channelId)
        {
            _channelService.RemoveChannel(1, channelId);
            return RedirectToAction("ChannelList");
        }

        public ActionResult RefreshChannelFeeds()
        {

            return RedirectToAction("ChannelList");
        }
    }
}