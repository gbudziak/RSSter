﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RSS
{
    public class Item
    {
        public string Link { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string PublishDate { get; set; }
        public bool Read { get; set; }
        public bool ReadLater { get; set; }
        public int Raiting { get; set; }
    }
}
