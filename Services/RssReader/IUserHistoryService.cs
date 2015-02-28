using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.RSS;
using Models.ViewModels;

namespace Services.RssReader
{
    public interface IUserHistoryService
    {
        void AddToHistory(HistoryAction actionName, DateTime date, long userChannelId, long userItemId, string subscriptionId, string userId);
        
        List<UserHistoryViewModel> ShowUserHistory(string userId,string userName);

    }
}
