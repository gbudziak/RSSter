using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS
{
    public class Channel
    {
        public List<Item> Items { get; set; }
        public string Link { get; set; }
    }
}
