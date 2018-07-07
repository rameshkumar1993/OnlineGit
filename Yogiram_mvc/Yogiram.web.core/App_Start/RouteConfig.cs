using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Yogiram.web.core
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 "Default",
                 "{controller}/{action}/{id}",
                new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                new string[] { "Yogiram.web.core.Controllers" }
            );
        }
    }
}
