using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.RssReader;

namespace RSSter.Controllers
{
    [Authorize]
    public class AjaxController : Controller
    {
        private readonly IItemService _itemService;

        public AjaxController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public JsonResult RatingUp(long userItemId)
        {
            var response = _itemService.IncreaseUserRating(userItemId);
            return Json(response);
        }

        [HttpPost]
        public JsonResult RatingDown(long userItemId)
        {
            var response = _itemService.DecreaseUserRating(userItemId);
            return Json(response);
        }

        [HttpPost]
        public JsonResult Read(long userItemId)
        {
            _itemService.MarkAsRead(userItemId);
            return Json(null);
        }
    }
}