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


        public void AddToHistory(string actionName, DateTime date, long userChannelId, long userItemId, string subscriptionId, string userId)
        {
            var userHistory = new UserHistory() { ActionName = actionName, ApplicationUserId = userId, Date = date, SubscriptionId = subscriptionId, UserChannelId = userChannelId, UserItemId = userItemId };
            _rssDatabase.UsersHistory.Add(userHistory);
            _rssDatabase.SaveChanges();

        }

    }
}
