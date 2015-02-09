using System;
using System.Collections.Generic;
using System.Linq;
using Models.RSS;
using Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Services.RssReader
{
    public interface  ISearchResultsBuilder
    {
        IEnumerable<Channel> GetAllChannels();
        IEnumerable<Item> GetAllItems();
        List<IdentityUser> GetAllUsers();
        Search MainSearch(string searchString);
        List<IdentityUser> SearchUsersForString(string searchString);
        List<SearchChannel> SearchChannelsForString(string searchString);
    }
}
