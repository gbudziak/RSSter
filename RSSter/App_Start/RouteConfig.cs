using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RSSter
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: "signin-google", url: "signin-google", defaults: new { controller = "Account", action = "LoginCallback" });
            //routes.MapRoute(name: "signin-facebook", url: "signin-facebook", defaults: new { controller = "Account", action = "LoginCallback" }); 

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "LoginIndex", id = UrlParameter.Optional }
            );
        }
    }
}
