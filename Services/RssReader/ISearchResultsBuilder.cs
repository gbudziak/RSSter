using System;
using System.Collections.Generic;
using System.Linq;
using Models.RSS;

namespace Services.RssReader
{
    public interface  ISearchResultsBuilder
    {
        List<Channel> GetAllChannels();
        List<Item> GetAllItems();
        List<Channel> SearchForString(string searchString);
    }

}
