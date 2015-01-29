using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CompleteChannelInfo
    {
        public long ChannelId { get; set; }
        public long UserChannelId { get; set; }

        public string Url { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public long Readers { get; set; }

        public bool IsHidden { get; set; }

    }
}
