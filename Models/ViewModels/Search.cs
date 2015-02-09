using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class Search
    {
        public List<SearchChannel> ChannelList{get; set;}
        public List<ApplicationUser> User { get; set; }
    }
}
