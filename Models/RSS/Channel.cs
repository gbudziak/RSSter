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
        public long ChannelId { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public Channel() { }

        public Channel(List<Item> items, string link)
        {
            this.Items = items;
            this.Link = link;
        }

        public Channel(string link)
        {
            this.Link = link;
            this.Items = new List<Item>();
        }
    }
}
