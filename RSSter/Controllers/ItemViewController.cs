using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Models.User;
using Models.ViewModels;
using Services.RssReader;

namespace RSSter.Controllers
{
    [Authorize]
    public class ItemViewController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IViewService _viewService;

        public ItemViewController(IItemService itemService, IViewService viewService)
        {
            _itemService = itemService;
            _viewService = viewService;
        }
        /*
        public ActionResult ViewItem(long userItemId, string viewType)
        {
            var userItem = _itemService.FetchUserItem(userItemId);
            var userId = User.Identity.GetUserId();
            UserCustomView returnView;
            switch (viewType)
            {
                case "basic":
                    returnView = DefaultViews.Simple;
                    break;
                case "full":
                    returnView = DefaultViews.Full;
                    break;
                case "custom1":
                    returnView = _viewService.FetchView(userId);
                    break;
                default:
                    return PartialView("ErrorView");
            }
            var userItemView = new UserItemsViewModel(returnView, userItem);

            return PartialView("ViewItem", userItemView);
        }
        */
    }
}