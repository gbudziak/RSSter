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
        private readonly IChannelService _channelService;
        private readonly IItemService _itemService;

        public AjaxController(IChannelService channelService, 
            IItemService itemService)
        {
            _channelService = channelService;
            _itemService = itemService;
        }

        [HttpPost]
        public JsonResult RatingUp(long userItemId)
        {
            _itemService.IncreaseUserRating(userItemId);
            return Json(null);
        }

        [HttpPost]
        public JsonResult RatingDown(long userItemId)
        {
            _itemService.DecreaseUserRating(userItemId);
            return Json(null);
        }

        [HttpPost]
        public JsonResult Read(long userItemId)
        {
            _itemService.MarkAsRead(userItemId);
            return Json(null);
        }
    }
}