using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models.User;
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
        private readonly IUserService _userService;
        private readonly ISubscriptionService _subscriptionService;


        public RssReaderController(IChannelService channelService, IItemService itemService, IUserService userService, ISubscriptionService subscriptionService)
        {
            _channelService = channelService;
            _itemService = itemService;
            _userService = userService;
            _subscriptionService = subscriptionService;

        }

        public ActionResult Index()
        {
            return View();
        }

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

        public ActionResult AddUserSubscription(string subscriptionId, string subscriptionEmail)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                _subscriptionService.AddSubscription(userId, subscriptionId, subscriptionEmail);
                var model =_subscriptionService.GetSubscriptionModel(subscriptionId);

                return View("ShowUserSubscriptionPage", model);
               
            }
            return RedirectToAction("Index", "Search");
        }

        public ActionResult ShowChannelItems(long channelId)
        {
            //var userId = User.Identity.GetUserId();
            var channelWithItems = _itemService.GetChannelWithItems(channelId);

            return View("ShowChannelItems", channelWithItems);
        }

        public ActionResult ShowUserItems(long userChannelId, int page = 1, int pageSize = 20, UserViewType viewType = UserViewType.Simple)
        {
            var userId = User.Identity.GetUserId();
            var userItemList = _itemService.GetUserChannelItems(userChannelId, userId, viewType, page, pageSize);

            return View("ShowUserItems", userItemList);
        }

        //public ActionResult ShowUserItemsByUrl(string url)
        //{
        //    var userId = User.Identity.GetUserId();
        //    var userChannelId = _channelService.ReturnUserChannelId(url, userId);
        //    ViewBag.UserChannelId = userChannelId;

        //    return View("ShowUserItems", _itemService.GetUserChannelItems(userChannelId, userId, 1, 20, 1));
        //}

        public ActionResult Delete(long userChannelId)
        {
            var userId = User.Identity.GetUserId();
            _channelService.RemoveChannel(userId, userChannelId);
            return RedirectToAction("Index", "Search");
        }

        public ActionResult ShowUserChannels()
        {
            var userId = User.Identity.GetUserId();

            var userChannelsViewModel = _channelService.GetUserChannels(userId);
            var returnModel = userChannelsViewModel.OrderByDescending(x => x.UnreadItemsCount).ToList();
            return PartialView("ShowUserChannels", returnModel);
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

        public ActionResult SetUserCustomView()
        {
            var sampleCompleteItemInfo = _itemService.GetSampleCompleteItemInfo();

            return View("SetUserCustomView", sampleCompleteItemInfo);
        }

        [HttpPost]
        public ActionResult SetUserCustomView(UserCustomView userCustomView)
        {
            if (ModelState.IsValid)
            {
                userCustomView.UserId = User.Identity.GetUserId();
                _userService.SetUserCustomView(userCustomView);
                return RedirectToAction("Index", "Search");
            }
            return View("SetUserCustomView");
        }

        public ActionResult ShowUserSubscriptions()
        {
            var userId = User.Identity.GetUserId();

            var subscriptions = _subscriptionService.GetUserSubscriptions(userId);
            return PartialView("ShowUserSubscriptions", subscriptions);
        }

        public ActionResult ShowUserSubscriptionPage(string subscriptionid)
        {


            var subscriptions = _subscriptionService.GetSubscriptionModel(subscriptionid);
            return PartialView("ShowUserSubscriptionPage", subscriptions);
        }
    }
}