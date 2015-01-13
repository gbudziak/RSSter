using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS.Interfaces
{
    interface IChannel
    {
        List<Item> Items { get; set; }
        string Link { get; set; }

        void PupulateItems();
    }
}
