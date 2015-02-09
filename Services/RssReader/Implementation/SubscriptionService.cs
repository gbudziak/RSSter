using Microsoft.AspNet.Identity.EntityFramework;
using Models.RSS;
using RssDataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RssReader.Implementation
{
    public class SubscriptionService : ISubscriptionService
    {

        private readonly IApplicationRssDataContext _rssDatabase;
        //private readonly IdentityDbContext _userDatabase = new IdentityDbContext();

        public SubscriptionService(IApplicationRssDataContext rssDatabase)
        {
            _rssDatabase = rssDatabase;
        }


        public void AddSubscription(string userId, string subscriptionId, string subscriptionEmail)
        {
            var newSubscription = new UserSubscriptions(userId,subscriptionId, subscriptionEmail);
            _rssDatabase.AllUserSubscriptions.Add(newSubscription);
            _rssDatabase.SaveChanges();


        }



    }
}
