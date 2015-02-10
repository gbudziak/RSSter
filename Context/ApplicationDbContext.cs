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
        
        public ApplicationRssDataContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public IDbSet<UserCustomView> UsersCustomViews { get; set; }

        public IDbSet<UserInfo> UserInfos { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();            
        }

        public IRssTransaction OpenTransaction()
        {
            return new RssTransaction(this.Database);
        }

        public IDbSet<Channel> Channels { get; set; }

        public IDbSet<UserChannel> UserChannels { get; set; }

        public IDbSet<UserItem> UsersItems { get; set; }

        public IDbSet<Item> AllItems { get; set; }

        public IDbSet<UserSubscriptions> AllUserSubscriptions { get; set; }

        public IDbSet<UserHistory> UsersHistory { get; set; }



        public static ApplicationRssDataContext Create()
        {
            return new ApplicationRssDataContext();
        }
    }
}