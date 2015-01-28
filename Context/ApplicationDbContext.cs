using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using RssDataContext;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Models;
using Models.RSS;
using Models.User;

namespace RssDataContext
{

    public class ApplicationRssDataContext :
        IdentityDbContext<ApplicationUser>, IApplicationRssDataContext
    {
        
        //static private List<Channel> _channels;
        
        //public List<Channel> Channels
        //{
        //    get { return _channels; }
        //    set { _channels = value; }
        //}
   
        //static Database()
        //{
        //    _channels = new List<Channel>(); 
        //}

        public ApplicationRssDataContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
           
        }

        public DbSet<UserCustomView> UsersCustomViews { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();            
        }

        public IRssTransaction OpenTransaction()
        {
            return new RssTransaction(this.Database);
        }

        public DbSet<Channel> Channels { get; set; }

        public DbSet<UserChannel> UserChannels { get; set; }

        public DbSet<UserItem> UsersItems { get; set; }

        public DbSet<Item> AllItems { get; set; }

        public static ApplicationRssDataContext Create()
        {
            return new ApplicationRssDataContext();
        }
    }
}