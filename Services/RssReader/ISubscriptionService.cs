using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.RssReader
{
    public interface ISubscriptionService
    {
        void AddSubscription(string userId, string subscriptionId, string subscriptionEmail);

    }
}
