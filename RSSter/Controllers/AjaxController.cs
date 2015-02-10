using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.RssReader;
using Microsoft.AspNet.Identity;

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
            var userId = User.Identity.GetUserId();

            var response = _itemService.IncreaseUserRating(userItemId,userId);
            return Json(response);
        }

        [HttpPost]
        public JsonResult RatingDown(long userItemId)
        {
            var userId = User.Identity.GetUserId();

            var response = _itemService.DecreaseUserRating(userItemId, userId);
            return Json(response);
        }

        [HttpPost]
        public JsonResult Read(long userItemId)
        {
            var userId = User.Identity.GetUserId();

            _itemService.MarkAsRead(userItemId, userId);
            return Json(null);
        }
    }
}