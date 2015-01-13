using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS
{
    public class ChannelList : Interfaces.IChannelList
    {
        public List<Channel> Channels { get; set; }

        public void AddChannel(string link)
        {
            throw new NotImplementedException();
        }

        public void DeleteChannel(string link)
        {
            throw new NotImplementedException();
        }
    }
}
