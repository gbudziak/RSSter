﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Models.RSS
{
    public class Channel 
    {
        [Key]
        public long Id { get; set; }
        public List<Item> Items { get; set; }
        
        //[Remote("LinkValidation","Validation")]
        public string Link { get; set; }
        
        public string Description { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public long Readers { get; set; }

        public Channel()
        {
            this.Items = new List<Item>();
        }

        public Channel(List<Item> items, string link)
        {
            this.Items = items;
            this.Link = link;
        }

        public Channel(string link)
        {
            this.Link = link;
            this.Items = new List<Item>();
        }
    }
}
