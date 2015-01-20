using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS
{
    public class UItem
    {
        [Key]
        public long Id { get; set; }
        public bool Read { get; set; }
        public bool RaitingPlus { get; set; }
        public bool RaitingMinus { get; set; }

        [ForeignKey("Item")]
        public long ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
