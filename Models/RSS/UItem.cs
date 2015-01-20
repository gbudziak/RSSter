using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS
{
    public class UItem
    {
        public long Id { get; set; }
        public bool Read { get; set; }
        public bool RaitingPlus { get; set; }
        public bool RaitingMinus { get; set; }
    }
}
