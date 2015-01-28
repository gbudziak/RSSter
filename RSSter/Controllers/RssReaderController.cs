using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Models.RSS;
using Services.RssReader;

namespace RSSter.Controllers
{
    [Authorize]
    public class RssReaderController : Controller
    {
        private readonly IChannelService _channelService;
        private readonly IItemService _itemService;

        public RssReaderController(IChannelService channelService, 
            IItemService itemService)
        {
            _channelService = channelService;
            _itemService = itemService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("ShowAllUserItems","RssReader");
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
                var userChannelId = _channelService.AddChannel(userId, url);                
                return RedirectToAction("ShowUserItems","RssReader", new { userChannelId = userChannelId});
            }
            return View("AddRssChannel");
        }

        public ActionResult ShowUserItems(long userChannelId)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.UserChannelId = userChannelId;
            return View(_itemService.GetUserChannelItems(userChannelId,userId));
        }

        public ActionResult ShowUserItemsByUrl(string url)
        {
            var userId = User.Identity.GetUserId();
            var userChannelId = _channelService.ReturnUserChannelId(url, userId);
            ViewBag.UserChannelId = userChannelId;

            return View("ShowUserItems", _itemService.GetUserChannelItems(userChannelId, userId));
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

            var items = _itemService.GetAllUserItems(userId);
            return View("ShowAllUserItems", items);
        }

        public ActionResult ShowAllUnreadUserItems()
        {
            var userId = User.Identity.GetUserId();

            var items = _itemService.GetAllUnreadUserItems(userId);
            return View("ShowAllUserItems", items);
        }

        public ActionResult MarkAllItemsAsRead()
        {
            var userId = User.Identity.GetUserId();
            _itemService.MarkAllItemsAsRead(userId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult MarkAllChannelItemsAsRead(long userChannelId)
        {
            var userId = User.Identity.GetUserId();
            _itemService.MarkAllChannelItemsAsRead(userId, userChannelId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult ViewChannelInfo(long userChannelId)
        {
            var userId = User.Identity.GetUserId();
            var channel = _channelService.GetChannel(userId, userChannelId);
            return PartialView("ViewChannelInfo", channel);
        }

        public ActionResult RefreshAllUserChannels()
        {
            var userId = User.Identity.GetUserId();
            var channels = _channelService.GetUserChannels(userId);
            foreach (var channel in channels)
            {
                RefreshChannelItems(channel.ChannelId);
            }

            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);

        }


        public ActionResult RefreshChannelItems(long userChannelId)
        {
            var userId = User.Identity.GetUserId();

            _channelService.AddNewItemsToChannel(userChannelId,userId);
            _channelService.AddNewItemsToUserChannel(userChannelId,userId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }
    }
}