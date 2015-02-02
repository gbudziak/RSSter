using Models.RSS;
using System;
using System.Linq;

namespace Models.ViewModels
{
    public class SearchChannel
    {
        public long Id { get; set; }
        public long Rating { get; set; }

        public string Url { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public long Readers { get; set; }



        public SearchChannel() : this(0, string.Empty, string.Empty, string.Empty,0)
        {
        }

        public SearchChannel(long rating, string url, string description, string title, long readers)
        {

            Rating = rating;
            Url = url;
            Description = description;
            Title = title;
            Readers = readers;            
        }

    }
}