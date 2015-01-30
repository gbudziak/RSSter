using Models.RSS;
using System;
using System.Linq;

namespace Models.ViewModels
{
    public class SearchChannel
    {
        //public long Id { get; set; }
        public long Rating { get; set; }


        //public string Url { get; set; }
        //public string Description { get; set; }
        //public string ImageUrl { get; set; }
        //public string Title { get; set; }
        //public long Readers { get; set; }


        public Channel Channel { get; set; }


        public SearchChannel(Channel channel, long rating)
        {
            Channel = channel;
            Rating += rating;
        }

    }
}