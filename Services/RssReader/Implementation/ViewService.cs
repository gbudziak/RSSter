using System.Linq;
using Models.User;
using RssDataContext;

namespace Services.RssReader.Implementation
{
    public class ViewService : IViewService
    {
        private readonly IApplicationRssDataContext _rssDatabase;

        public ViewService(IApplicationRssDataContext rssDatabase)
        {
            _rssDatabase = rssDatabase;
        }

        public UserCustomView FetchView(string userId)
        {
            var userCustomView = _rssDatabase.UsersCustomViews.First(userView => userView.UserId == userId);
            return userCustomView;
        }
    }
}