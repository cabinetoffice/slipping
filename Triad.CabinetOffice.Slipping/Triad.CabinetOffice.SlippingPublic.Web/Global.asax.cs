using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Triad.CabinetOffice.SlippingPublic.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            AntiForgeryConfig.SuppressXFrameOptionsHeader = true;
            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Logger.LogException(ex);
        }
    }
}
