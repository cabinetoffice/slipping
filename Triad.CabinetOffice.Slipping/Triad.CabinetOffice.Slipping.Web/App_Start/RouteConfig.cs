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
                url: "Slips",
                defaults: new { controller = "Slips", action = "Index" }
            );

            routes.MapRoute(
                name: "Review Slipping Request",
                url: "Slips/Review/{id}",
                defaults: new { controller = "Slips", action = "Review" }
            );

            routes.MapRoute(
                name: "Slip Cancelled",
                url: "Slips/Review/{id}/Cancelled",
                defaults: new { controller = "Slips", action = "Cancelled" }
            );

            routes.MapRoute(
                name: "Create Slipping Request",
                url: "Slips/Create",
                defaults: new { controller = "Slips", action = "FromDate", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Edit Slipping Request",
                url: "Slips/Edit/{id}/{action}",
                defaults: new { controller = "Slips" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
