using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.RSS;

namespace Services.RssReader
{
    public interface IUserHistoryService
    {
        void AddToHistory(string actionName, DateTime date, long userChannelId, long userItemId, string subscriptionId, string userId);
        List<UserHistory> ShowUserHistory(string userId);

    }
}
