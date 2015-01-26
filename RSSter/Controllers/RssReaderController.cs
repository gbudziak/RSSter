using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Index()
        {
            return RedirectToAction("ShowAllUserItems", "RssReader");
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

        public ActionResult ShowUserItems(long userChannelId)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.UserChannelId = userChannelId;
            return View(_channelService.GetUserItems(userChannelId,userId));
        }

        public ActionResult ShowUserItemsByUrl(string url)
        {
            var userId = User.Identity.GetUserId();
            var userChannelId = _channelService.ReturnUserChannelId(url, userId);

            return View("ShowUserItems", _channelService.GetUserItems(userChannelId, userId));
        }

        public ActionResult Delete(long channelId)
        {
            var userId = User.Identity.GetUserId();
            _channelService.RemoveChannel(userId, channelId);
            return RedirectToAction("Index");
        }

        public ActionResult ShowUserChannels()
        {
            var userId = User.Identity.GetUserId();

            var channels = _channelService.GetUserChannels(userId);
            return PartialView("ShowUserChannels", channels);
        }

        public ActionResult ShowAllUserItems()
        {
            var userId = User.Identity.GetUserId();

            var items = _channelService.GetAllUserItems(userId);
            return View("ShowAllUserItems", items);
        }

        public ActionResult ShowAllUnreadUserItems()
        {
            var userId = User.Identity.GetUserId();

            var items = _channelService.GetAllUnreadUserItems(userId);
            return View("ShowAllUserItems", items);
        }

        public ActionResult MarkAllItemsAsRead(long userChannelId)
        {
            var userId = User.Identity.GetUserId();
            _channelService.MarkAllItemsAsRead(userId,userChannelId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        [HttpPost]
        public JsonResult RatingUp(long userItemId)
        {
            _channelService.AddRating(userItemId);
            return Json(null);
        }

        [HttpPost]
        public JsonResult RatingDown(long userItemId)
        {
            _channelService.RemoveRating(userItemId);
            return Json(null);
        }

        [HttpPost]
        public JsonResult Read(long userItemId)
        {
            _channelService.MarkAsRead(userItemId);
            return Json(null);
        }

        public ActionResult ViewChannelInfo(long userChannelId)
        {
            var userId = User.Identity.GetUserId();
            var channel = _channelService.GetChannelInfo(userId, userChannelId);
            return PartialView("ViewChannelInfo", channel);
        }
    }
}