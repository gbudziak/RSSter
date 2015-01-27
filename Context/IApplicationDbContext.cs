using System.Collections.Generic;
using System.Data.Entity;
using RssDataContext;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.RSS;

namespace RssDataContext
{
    public interface IApplicationRssDataContext
    {        
        DbSet<Channel> Channels { get; set; }
        DbSet<UserChannel> UserChannels { get; set; }
        DbSet<UserItem> UsersItems { get; set; }
        DbSet<Item> AllItems { get; set; }

        void SaveChanges();


        IRssTransaction OpenTransaction();
    }
}

   