using Models.RSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class UserHistoryViewModel
    {
        public DateTime Date { get; set; }
        public string SubscriptionId { get; set; }
        public long UserChannelId { get; set; }
        public long UserItemId { get; set; }

        public HistoryAction HistoryActionName { get; set; }
        public Channel Channel { get; set; }
        public Item Item { get; set; }


    }
}
