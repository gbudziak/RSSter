using PagedList;

namespace Models.ViewModels
{
    public class ChannelItemsViewModel
    {
        public PagedList<CompleteItemInfo> Items { get; set; }

        public long ChannelId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public long Readers { get; set; }
        public long TotalPosts { get; set; }
        public double PostsPerDay { get; set; }
        public string LastPost { get; set; }
    }
}
