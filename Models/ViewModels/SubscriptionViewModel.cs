using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.RSS;

namespace Models.ViewModels
{
    public class SubscriptionViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<UserChannel> Channels { get; set; }

    }
}
