using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Models.RSS
{
    public class UserItem
    {
        [Key]
        public long Id { get; set; }
        public bool Read { get; set; }
        public bool RaitingPlus { get; set; }
        public bool RaitingMinus { get; set; }

        [ForeignKey("Item")]
        public long ItemId { get; set; }
        public virtual Item Item { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("UserChannel")]
        public long UserChannelId { get; set; }
        public virtual UserChannel UserChannel { get; set; }

        public UserItem() { }

        public UserItem(string applicationUserId, long itemId)
        {
            this.ApplicationUserId = applicationUserId;
            this.ItemId = itemId;

        }

        public UserItem(string applicationUserId, long itemId, long userChannelId)
        {
            this.ApplicationUserId = applicationUserId;
            this.ItemId = itemId;
            this.UserChannelId = userChannelId;
        }
    }
}
