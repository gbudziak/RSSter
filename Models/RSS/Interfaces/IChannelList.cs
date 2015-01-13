using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS.Interfaces
{
    interface IChannelList
    {
        List<Channel> Channels { get; set; }

        void AddChannel(string link);

        void DeleteChannel(string link);
    }
}
