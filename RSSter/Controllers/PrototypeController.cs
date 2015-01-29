using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Models.RSS;
using Models.ViewModels;
using RssDataContext;
using Services.RssReader;

namespace RSSter.Controllers
{
    [Authorize]
    public class PrototypeController : Controller
    {
        private readonly IApplicationRssDataContext _rssDatabase;
        private readonly IViewService _viewService;

        public PrototypeController(IApplicationRssDataContext rssDatabase, IViewService viewService)
        {
            _rssDatabase = rssDatabase;
            _viewService = viewService;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var items = _rssDatabase.AllItems.ToList();
            var userItems = _rssDatabase.UsersItems.Where(ui => ui.ApplicationUserId == userId).ToList();
            var output = new List<CompleteItemInfo>();
            foreach (var ui in userItems)
            {
                var i = items.First(item => item.Id == ui.ItemId);
                var o = Mapper.Map<UserItem, CompleteItemInfo>(ui);
                Mapper.Map<Item, CompleteItemInfo>(i, o);
                o.ItemAge = _viewService.CalculateItemAge(i.PublishDate);
                output.Add(o);
            }
            var sortedOutput = output.OrderBy(o => o.ItemAge);

            return View("Index", sortedOutput);
        }
    }
}