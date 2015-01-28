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
    public class UserChannel
    {
        [Key]
        public long Id { get; set; }
        public string Category { get; set; }
        public List<UserItem> UserItems { get; set; }
        public bool IsHidden { get; set; }

        [ForeignKey("Channel")]
        public long ChannelId { get; set; }
        public virtual Channel Channel { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public UserChannel():this(-1,"")
        {}

        public UserChannel(long channelId, string applicationUserId)
        {
            this.ChannelId = channelId;
            this.ApplicationUserId = applicationUserId;
            this.UserItems = new List<UserItem>();

        }
    }
}
