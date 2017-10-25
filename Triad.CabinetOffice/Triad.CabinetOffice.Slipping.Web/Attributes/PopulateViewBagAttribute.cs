using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Triad.CabinetOffice.Slipping.Web.Attributes
{
    public class PopulateViewBagAttribute : ActionFilterAttribute
    {
        internal static void PopulateViewBag(dynamic viewBag)
        {
            viewBag.FeedbackUrl = WebConfigurationManager.AppSettings["FeedbackUrl"];
            viewBag.GuidanceUrl = WebConfigurationManager.AppSettings["GuidanceUrl"];
            viewBag.ContactEmail = WebConfigurationManager.AppSettings["ContactEmail"];
            viewBag.ContactTelephone = WebConfigurationManager.AppSettings["ContactTelephone"];
            viewBag.GoogleAnalyticsID = WebConfigurationManager.AppSettings["GoogleAnalyticsID"];
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            PopulateViewBag(filterContext.Controller.ViewBag);
        }
    }
}