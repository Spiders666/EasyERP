using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EasyERP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "EasyERP.Controllers" }
            );
            // will allow for Products/Item/2/1
            routes.MapRoute(
                    "ProductsList",
                    "Products/List/{id}/{category}",
                    new { controller = "Products", action = "List" }
            );
        }
    }
}
