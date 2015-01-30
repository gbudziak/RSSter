namespace Models.ViewModels
{
    public class ShowUserChannelsViewModel
    {
        public long ChannelId { get; set; }
        public long UserChannelId { get; set; }

        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }

        public int UnreadItemsCount { get; set; }
        public int AllItemsCount { get; set; }

    }
}
