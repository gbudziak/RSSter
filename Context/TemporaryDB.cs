using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Models.RSS;

namespace DBContext
{
    public static class TemporaryDb
    {
        public static ChannelList TempDb { get; set; }

        static TemporaryDb()
        {
            TempDb = new ChannelList(1);
        }
    }
}
