using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models.RSS;
using Services.RssReader;

namespace RSSter.Controllers
{
    [Authorize]
    public class RssReaderController : Controller
    {
        private readonly IChannelService _channelService;

        public RssReaderController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        [Authorize]
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
        public ActionResult AddRssChannel(string url)
        {
            if (ModelState.IsValid)
            {
                
                var userId = User.Identity.GetUserId();                
                _channelService.AddChannel(userId, url);

                return RedirectToAction("Index","RssReader");
            }
            return View("AddRssChannel");
        }

        public ActionResult RssListView(long userChannelId)
        {
            var userId = User.Identity.GetUserId();                

            return View(_channelService.ShowChannelFeedList(userChannelId,userId));
        }

        public ActionResult ChannelList()
        {
            var channels = _channelService.ShowChannelList();
            return View(channels);
        }

        public ActionResult Delete(long userChannelId)
        {
            var userId = User.Identity.GetUserId();
            _channelService.RemoveChannel(userId, userChannelId);
            return RedirectToAction("ChannelList");
        }

        public ActionResult Channels()
        {
            var userId = User.Identity.GetUserId();

            var channels = _channelService.GetChannels(userId);
            return PartialView("Channels", channels);
        }

        public JsonResult RaitingUp(long userItemId)
        {
            var result = _channelService.AddRaiting(userItemId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RaitingDown(long userItemId)
        {
            var result = _channelService.RemoveRaiting(userItemId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Read(long userItemId)
        {
            var result = _channelService.MarkAsRead(userItemId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}