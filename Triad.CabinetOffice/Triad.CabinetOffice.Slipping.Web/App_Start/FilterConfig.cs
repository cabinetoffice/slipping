using System.Web;
using System.Web.Mvc;
using Triad.CabinetOffice.Slipping.Web.Attributes;

namespace Triad.CabinetOffice.Slipping.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LoggingHandleErrorAttribute());
        }
    }
}
