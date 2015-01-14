using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS
{
    public class ChannelList
    {
        public List<Channel> Channels { get; set; }
        public int Id { get; set; }

        public ChannelList(int id)
        {
            this.Channels = new List<Channel>();
            this.Id = id;
        }

        public ChannelList()
        {
            this.Channels = new List<Channel>();
        }

    }
}
