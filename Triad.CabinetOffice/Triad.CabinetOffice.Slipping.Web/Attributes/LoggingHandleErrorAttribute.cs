using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triad.CabinetOffice.Slipping.Web.Attributes
{
    public class LoggingHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Logger.LogException(filterContext.Exception);

            if (filterContext.Result is ViewResult)
            {
                PopulateViewBagAttribute.PopulateViewBag((filterContext.Result as ViewResult).ViewBag);
            }
        }
    }
}