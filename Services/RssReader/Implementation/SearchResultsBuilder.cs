using System;
using System.Collections.Generic;
using System.Linq;
using Models.RSS;
using RssDataContext;

namespace Services.RssReader.Implementation
{
    public class  SearchResultsBuilder:ISearchResultsBuilder
    {
        private readonly IApplicationRssDataContext _rssDatabase;

        public SearchResultsBuilder(IApplicationRssDataContext rssDatabase)
        {
            _rssDatabase = rssDatabase;

        }

        public List<Channel> GetAllChannels()
        {
            var model = _rssDatabase.Channels.Select(x => x).ToList();
            return model;
        }

        public List<Item> GetAllItems()
        {
            var model = _rssDatabase.AllItems.Select(x => x).ToList();
            return model;
        }

        public List<Channel> SearchForString(string searchString)
        {
            var model = GetAllChannels();
            var result = model.Where(x => x.Description.Contains(searchString) || x.Title.Contains(searchString) || x.Url.Contains(searchString)).OrderBy(x => x.Readers).Take(10).ToList();

            return result;

        }


    }
                  
}
