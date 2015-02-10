using Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS
{
    public class UsersHistory
    {
        [Key]
        public long Id { get; set; }

        public HistoryAction ActionName { get; set; }
        public DateTime Date { get; set; }
        public long UserChannelId { get; set; }
        public long USerItemId { get; set; }
        public string SubscriptionId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public enum HistoryAction
    {
        Read,
        AddChannel,
        RemoveChannel,
        AddSubscription,
        RemoveSubscription,
        RatingPlus,
        RatingMinus
    }     
}
