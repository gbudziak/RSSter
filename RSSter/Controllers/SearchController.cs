using System;
using System.Linq;
using System.Web.Mvc;
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

        public ActionResult Index(string id)
        {


            if (!String.IsNullOrEmpty(id))
            {
                _channelService.AddChannel(id);

                var result = _searchResultBuilder.SearchForString(id);
                return View(result);
            }
            
            return View();
        }
    }
}