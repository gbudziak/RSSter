using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS
{
    public class UChannel
    {
        [Key]
        public long Id { get; set; }
        public string Category { get; set; }
        public List<UItem> UItems { get; set; }

        [ForeignKey("Channel")]
        public long ChannelId { get; set; }
        public virtual Channel Channel { get; set; }

        public UChannel() { }

        public UChannel(List<UItem> uitems, long id)
        {
            this.UItems = uitems;
            this.Id = id;
        }

        public UChannel(long id)
        {
            this.Id = id;
            this.UItems = new List<UItem>();
        }
    }
}
