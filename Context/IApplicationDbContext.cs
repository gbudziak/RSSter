using System.Data.Entity;
using Models.RSS;
using Models.User;

namespace RssDataContext
{
    public interface IApplicationRssDataContext
    {        
        DbSet<Channel> Channels { get; set; }
        DbSet<UserChannel> UserChannels { get; set; }
        DbSet<UserItem> UsersItems { get; set; }
        DbSet<Item> AllItems { get; set; }
        DbSet<UserCustomView> UsersCustomViews { get; set; }
        DbSet<UserInfo> UserInfos { get; set; }

        void SaveChanges();

        IRssTransaction OpenTransaction();
    }
}

   