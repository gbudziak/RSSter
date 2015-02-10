using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.RSS;
using Models.ViewModels;

namespace Services.RssReader
{
    public interface ISubscriptionService
    {
        void AddSubscription(string userId, string subscriptionId, string subscriptionEmail);

        SubscriptionViewModel GetSubscriptionModel(string subscriptionId);


        List<UserSubscriptions> GetUserSubscriptions(string userId);
    }
}
