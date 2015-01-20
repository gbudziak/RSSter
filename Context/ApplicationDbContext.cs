using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Models;
using Models.RSS;

namespace DBContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
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

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
           
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
           
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public DbSet<Channel> Channels { get; set; }

        public DbSet<UserChannel> UserChannels { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}