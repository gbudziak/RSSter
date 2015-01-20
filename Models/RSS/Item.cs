using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS
{
    public class Item
    {
        [Key]
        public string Link { get; set; }

        public string Description { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string PublishDate { get; set; }
        public int RaitingPlus { get; set; }
        public int RaitingMinus { get; set; }

        //public bool Read { get; set; }
        //public bool ReadLater { get; set; }
    }
}
