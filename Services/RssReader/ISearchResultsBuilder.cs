using System;
using System.Collections.Generic;
using System.Linq;
using Models.RSS;
using Models.ViewModels;

namespace Services.RssReader
{
    public interface  ISearchResultsBuilder
    {
        IEnumerable<Channel> GetAllChannels();
        IEnumerable<Item> GetAllItems();
        List<SearchChannel> SearchForString(string searchString);
    }

}
