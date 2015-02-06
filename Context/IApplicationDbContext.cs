using System.Data.Entity;
using Models.RSS;
using Models.User;

namespace RssDataContext
{
    public interface IApplicationRssDataContext
    {        
        IDbSet<Channel> Channels { get; set; }
        IDbSet<UserChannel> UserChannels { get; set; }
        IDbSet<UserItem> UsersItems { get; set; }
        IDbSet<Item> AllItems { get; set; }
        IDbSet<UserCustomView> UsersCustomViews { get; set; }
        IDbSet<UserInfo> UserInfos { get; set; }

        void SaveChanges();

        IRssTransaction OpenTransaction();
    }
}

   