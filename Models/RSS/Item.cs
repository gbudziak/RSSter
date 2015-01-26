using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS
{
    public class Item
    {
        [Key]
        public long Id { get; set; }    
        public string Url { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }        
        public DateTime PublishDate { get; set; }
        public int RatingPlus { get; set; }
        public int RatingMinus { get; set; }

        [ForeignKey("Channel")]
        public long ChannelId { get; set; }
        public virtual Channel Channel { get; set; }


    }
}
