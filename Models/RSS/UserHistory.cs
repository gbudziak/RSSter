namespace Models.RSS
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Models.Models;

    public class UserHistory
    {
        [Key]
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long UserChannelId { get; set; }
        public long UserItemId { get; set; }
        public string SubscriptionId { get; set; }
        public HistoryAction HistoryActionName { get; set; }

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
