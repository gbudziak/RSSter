﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS
{
    public class Item
    {
        [Key]
        public long Id { get; set; }    
        public string Url { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }        
        public string PublishDate { get; set; }
        public int RaitingPlus { get; set; }
        public int RaitingMinus { get; set; }                
    }
}
