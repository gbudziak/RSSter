using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ShowAllUserItemsViewModel
    {
        public long ItemId { get; set; }
        public long UserItemId { get; set; }

        public bool Read { get; set; }
        public bool RatingPlus { get; set; }
        public bool RatingMinus { get; set; }

        public string Url { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public int RatingPlusCount { get; set; }
        public int RatingMinusCount { get; set; }

        public string ChannelTitle { get; set; }

        public TimeSpan ItemAge { get; set; }
    }
}
