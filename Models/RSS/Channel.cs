using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.RSS
{
    public class Channel
    {
        [Key]
        public long Id { get; set; }
        public List<Item> Items { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public long Readers { get; set; }

        public Channel()
            : this(string.Empty, string.Empty, string.Empty, string.Empty, new List<Item>(), 0)
        { }

        public Channel(string link)
            : this(link, string.Empty, string.Empty, string.Empty, new List<Item>(), 0)
        { }

        public Channel(string url, string title, string description, string imageUrl, List<Item> itemList, long readersCount)
        {
            Url = url;
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            Items = itemList;
            Readers = readersCount;
        }
    }
}
