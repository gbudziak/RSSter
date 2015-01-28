using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace Models.User
{
    public class UserCustomView
    {
        public long Id { get; set; }
        public bool Image { get; set; }
        public bool Title { get; set; }
        public bool Description { get; set; }
        public bool PublishDate { get; set; }
        public bool Rating { get; set; }
        //size
        //color
        //font

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public UserCustomView() { }

        public UserCustomView(string userId, bool image, 
            bool title, bool description, 
            bool publishDate, bool rating)
        {
            this.UserId = userId;
            this.Image = image;
            this.Title = title;
            this.Description = description;
            this.PublishDate = publishDate;
            this.Rating = rating;
        }
    }
}
