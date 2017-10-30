using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.PAWS2;

namespace Triad.CabinetOffice.PAWS.API
{
    public static class WebApiConfig
    {
        private static string pawsCorsOrigin = ConfigurationManager.AppSettings["PawsCorsOrigin"];
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors(new EnableCorsAttribute(pawsCorsOrigin, "*", "*") { SupportsCredentials = true });

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Session>("Sessions");
            builder.EntitySet<Division>("Divisions");
            builder.EntitySet<User>("Users");
            builder.EntitySet<Party>("Parties");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
