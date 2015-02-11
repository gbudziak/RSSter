using Models.RSS;
using RssDataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RssReader.Implementation
{
    public class UserHistoryService : IUserHistoryService
    {

        private readonly IApplicationRssDataContext _rssDatabase;


        public UserHistoryService(IApplicationRssDataContext rssDatabase)
        {
            _rssDatabase = rssDatabase;
        }

        public void AddToHistory(HistoryAction actionName, DateTime date, long userChannelId, long userItemId, string subscriptionId, string userId)
        {
            var userHistory = new UserHistory() { HistoryActionName = actionName, ApplicationUserId = userId, Date = date, SubscriptionId = subscriptionId, UserChannelId = userChannelId, UserItemId = userItemId };
            _rssDatabase.UsersHistory.Add(userHistory);
            _rssDatabase.SaveChanges();

        }

        public List<UserHistory> ShowUserHistory(string userId)
        {
            var model = _rssDatabase.UsersHistory.Where(history => history.ApplicationUserId == userId)
                .ToList();
            return model;
        }



    }
}
