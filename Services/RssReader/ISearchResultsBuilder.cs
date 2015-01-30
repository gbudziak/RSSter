using System;
using System.Collections.Generic;
using System.Linq;
using Models.RSS;

namespace Services.RssReader
{
    public interface  ISearchResultsBuilder
    {
        IEnumerable<Channel> GetAllChannels();
        IEnumerable<Item> GetAllItems();
        IEnumerable<Channel> SearchForString(string searchString);
    }

}
