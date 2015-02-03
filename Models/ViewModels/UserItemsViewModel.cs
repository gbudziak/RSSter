using System;
using System.Collections.Generic;
using Models.RSS;
using Models.User;

namespace Models.ViewModels
{
    public class UserItemsViewModel
    {
        public List<CompleteItemInfo> Items { get; set; }

        public long UserChannelId { get; set; }

        public long ChannelId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public long Readers { get; set; }
        public long TotalPosts { get; set; }
        public double PostsPerDay { get; set; }
        public TimeSpan LastPost { get; set; }

    }
}
