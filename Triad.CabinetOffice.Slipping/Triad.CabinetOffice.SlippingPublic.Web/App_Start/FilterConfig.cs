using System.Web;
using System.Web.Mvc;
using Triad.CabinetOffice.SlippingPublic.Web.Attributes;

namespace Triad.CabinetOffice.SlippingPublic.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LoggingHandleErrorAttribute());
            filters.Add(new PopulateViewBagAttribute());
        }
    }
}
