using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.RssReader;

namespace RSSter.Controllers
{
    [Authorize]
    public class ViewController : Controller
    {
        private readonly IItemService _itemService;

        public ViewController(IItemService itemService)
        {
            _itemService = itemService;
        }

        public ActionResult SimpleView(long userItemId)
        {
            return PartialView("SimpleView", _itemService.FetchUserItem(userItemId));
        }

        public ActionResult FullView(long userItemId)
        {
            return PartialView("FullView", _itemService.FetchUserItem(userItemId));
        }

        public ActionResult CustomView(long userItemId)
        {
            return PartialView("CustomView", _itemService.FetchUserItem(userItemId));
        }
    }
}