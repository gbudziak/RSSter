using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models.ViewModels;
using PagedList;
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
            return View();
        }

        //[HttpGet]
        //public ActionResult AddRssChannel()
        //{
        //    return View("AddRssChannel");
        //}

        //[HttpPost]
        public ActionResult AddRssChannel(string url)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();                
                var userChannelId =_channelService.AddChannel(userId, url);      
          
                return RedirectToAction("ShowUserItems","RssReader", new { userChannelId = userChannelId}); 
                //return RedirectToAction("Index","Search");
            }
            return RedirectToAction("Index", "Search");
        }

        public ActionResult ShowChannelItems(long channelId)
        {
            //var userId = User.Identity.GetUserId();
            var channelWithItems = _itemService.GetChannelWithItems(channelId);

            return View("ShowChannelItems", channelWithItems);
        }

        public ActionResult ShowUserItems(long userChannelId, int page = 1, int pageSize = 20, int viewType = 1)
        {
            var userId = User.Identity.GetUserId();
            var userItemList = _itemService.GetUserChannelItems(userChannelId, userId, viewType, page, pageSize);

            return View("ShowUserItems", userItemList);
        }

        public ActionResult ShowUserItemsByUrl(string url)
        {
            var userId = User.Identity.GetUserId();
            var userChannelId = _channelService.ReturnUserChannelId(url, userId);
            ViewBag.UserChannelId = userChannelId;

            return View("ShowUserItems", _itemService.GetUserChannelItems(userChannelId, userId, 1, 20, 1));
        }

        public ActionResult Delete(long userChannelId)
        {
            var userId = User.Identity.GetUserId();
            _channelService.RemoveChannel(userId, userChannelId);
            return RedirectToAction("Index");
        }

        public ActionResult ShowUserChannels()
        {
            var userId = User.Identity.GetUserId();

            var userChannelsViewModel = _channelService.GetUserChannels(userId);
            return PartialView("ShowUserChannels", userChannelsViewModel);
        }

        public ActionResult ShowAllUserItems(int page = 1, int pageSize = 20)
        {
            var userId = User.Identity.GetUserId();

            var allUserItems = _itemService.GetAllUserItems(userId);

            PagedList<ShowAllUserItemsViewModel> pagedViewModel = new PagedList<ShowAllUserItemsViewModel>(allUserItems, page, pageSize);

            return View("ShowAllUserItems", pagedViewModel);
        }

        public ActionResult ShowAllUnreadUserItems(int page = 1, int pageSize = 20)
        {
            var userId = User.Identity.GetUserId();

            var allUnreadUserItems = _itemService.GetAllUnreadUserItems(userId);

            PagedList<ShowAllUserItemsViewModel> pagedViewModel = new PagedList<ShowAllUserItemsViewModel>(allUnreadUserItems, page, pageSize);

            return View("ShowAllUnreadUserItems", pagedViewModel);
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

        public ActionResult RefreshAllUserChannels()
        {
            var userId = User.Identity.GetUserId();
            var channels = _channelService.GetUserChannels(userId);
            foreach (var completeChannelInfo in channels)
            {
                RefreshChannelItems(completeChannelInfo.UserChannelId, completeChannelInfo.ChannelId, completeChannelInfo.Url);
            }

            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);

        }

        public ActionResult RefreshChannelItems(long userChannelId, long channelId, string channelUrl)
        {
            var userId = User.Identity.GetUserId();

            _channelService.AddNewItemsToChannel(channelId,channelUrl);
            _channelService.AddNewItemsToUserChannel(userId, channelId,userChannelId);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }
    }
}