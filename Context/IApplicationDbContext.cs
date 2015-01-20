using System.Collections.Generic;
using System.Data.Entity;
using Models.RSS;

namespace DBContext
{
    public interface IApplicationDbContext
    {        
        DbSet<Channel> Channels { get; set; }       
    }   
}

   