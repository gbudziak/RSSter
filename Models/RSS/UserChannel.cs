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
        public List<UserItem> UItems { get; set; }

        [ForeignKey("Channel")]
        public long ChannelId { get; set; }
        public virtual Channel Channel { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public UserChannel() { }

        public UserChannel(List<UserItem> uitems, long id)
        {
            this.UItems = uitems;
            this.Id = id;
        }

        public UserChannel(long channelId, string applicationUserId)
        {
            this.ChannelId = channelId;
            this.ApplicationUserId = applicationUserId;
            this.UItems = new List<UserItem>();
        }
    }
}
