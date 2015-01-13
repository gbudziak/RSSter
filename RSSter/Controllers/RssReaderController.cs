﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Xml.Linq;
using Models.RSS;
using Services.RssReader;

//using Services.RssReader;

namespace RSSter.Controllers
{
    public class RssReaderController : Controller
    {
        private readonly IRssItemsList _rssItemsList;

        public RssReaderController(IRssItemsList rssItemsList)
        {
            _rssItemsList = rssItemsList;
        }


        public ActionResult RssListView(string blogUrl)
        {
            return View(_rssItemsList.GetRssFeed(blogUrl));
        }
    }
}