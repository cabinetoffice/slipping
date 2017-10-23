using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;

namespace Triad.CabinetOffice.PAWS.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors(new EnableCorsAttribute("https://localhost:4321", "*", "*") { SupportsCredentials = true });

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Session>("Sessions");
            builder.EntitySet<Division>("Divisions");
            builder.EntitySet<User>("Users");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
