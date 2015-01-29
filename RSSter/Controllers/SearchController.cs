using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RSSter.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string search)
        {

            if (!String.IsNullOrEmpty(search))
            {

                //movies = movies.Where(s => s.Title.Contains(searchString));
            } 

            return View();
        }
    }
}