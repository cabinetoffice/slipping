using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Triad.CabinetOffice.Slipping.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Slipping Requests",
                url: "Slipping",
                defaults: new { controller = "Slipping", action = "Index" }
            );

            routes.MapRoute(
                name: "Review Slipping Request",
                url: "Slipping/Review/{id}",
                defaults: new { controller = "Slipping", action = "Review" }
            );

            routes.MapRoute(
                name: "Create Slipping Request",
                url: "Slipping/Create",
                defaults: new { controller = "Slipping", action = "FromDate", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Edit Slipping Request",
                url: "Slipping/Edit/{id}/{action}",
                defaults: new { controller = "Slipping" }
            );

            routes.MapRoute(
                name: "Delete Slipping Request",
                url: "Slipping/Deleted/{date}",
                defaults: new { controller = "Slipping", action = "Deleted" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
