using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.RSS;

namespace DBContext
{
    public interface IApplicationDbContext
    {        
        DbSet<Channel> Channels { get; set; }
        DbSet<UserChannel> UserChannels { get; set; }
        DbSet<UserItem> UsersItems { get; set; }
        void SaveChanges();
    }   
}

   