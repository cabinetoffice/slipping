using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.PAWS;

namespace Triad.CabinetOffice.PAWS.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Absence_Request>("AbsenceRequest");
            builder.EntitySet<Absence_Request_Status>("AbsenceRequestStatus");
            builder.EntitySet<Absence_Request_Reason>("AbsenceRequestReason");
            builder.EntitySet<Members_of_Parliament>("MemberOfParliament");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

        }
    }
}
