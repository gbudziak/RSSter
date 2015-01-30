using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Models.RSS;
using Services.RssReader;

namespace RSSter.Controllers
{
    public class SearchController : Controller
    {
        private readonly IChannelService _channelService;
        private readonly IItemService _itemService;
        private readonly ISearchResultsBuilder _searchResultBuilder;

        public SearchController(IChannelService channelService, IItemService itemService, ISearchResultsBuilder searchResultBuilder)
        {
            _channelService = channelService;
            _itemService = itemService;
            _searchResultBuilder = searchResultBuilder;
        }

        public ActionResult Index(string searchString)
        {
            var result = new List<Channel>();
            if (!String.IsNullOrEmpty(searchString))
            {
                result = _searchResultBuilder.SearchForString(searchString);
                return View(result);
            }
            
            return View(result);
        }
    }
}