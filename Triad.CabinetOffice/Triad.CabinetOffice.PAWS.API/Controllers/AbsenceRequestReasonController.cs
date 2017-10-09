using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.PAWS;

namespace Triad.CabinetOffice.PAWS.API.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Triad.CabinetOffice.Slipping.Data.EntityFramework.PAWS;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Absence_Request_Reason>("AbsenceRequestReason");
    builder.EntitySet<Absence_Request>("Absence_Requests"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AbsenceRequestReasonController : ODataController
    {
        private PAWSEntities db = new PAWSEntities();

        // GET: odata/AbsenceRequestReason
        [EnableQuery]
        public IQueryable<Absence_Request_Reason> GetAbsenceRequestReason()
        {
            return db.Absence_Request_Reasons;
        }

        // GET: odata/AbsenceRequestReason(5)
        [EnableQuery]
        public SingleResult<Absence_Request_Reason> GetAbsence_Request_Reason([FromODataUri] int key)
        {
            return SingleResult.Create(db.Absence_Request_Reasons.Where(absence_Request_Reason => absence_Request_Reason.ID == key));
        }

        // PUT: odata/AbsenceRequestReason(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Absence_Request_Reason> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Absence_Request_Reason absence_Request_Reason = await db.Absence_Request_Reasons.FindAsync(key);
            if (absence_Request_Reason == null)
            {
                return NotFound();
            }

            patch.Put(absence_Request_Reason);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Absence_Request_ReasonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(absence_Request_Reason);
        }

        // POST: odata/AbsenceRequestReason
        public async Task<IHttpActionResult> Post(Absence_Request_Reason absence_Request_Reason)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Absence_Request_Reasons.Add(absence_Request_Reason);
            await db.SaveChangesAsync();

            return Created(absence_Request_Reason);
        }

        // PATCH: odata/AbsenceRequestReason(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Absence_Request_Reason> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Absence_Request_Reason absence_Request_Reason = await db.Absence_Request_Reasons.FindAsync(key);
            if (absence_Request_Reason == null)
            {
                return NotFound();
            }

            patch.Patch(absence_Request_Reason);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Absence_Request_ReasonExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(absence_Request_Reason);
        }

        // DELETE: odata/AbsenceRequestReason(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Absence_Request_Reason absence_Request_Reason = await db.Absence_Request_Reasons.FindAsync(key);
            if (absence_Request_Reason == null)
            {
                return NotFound();
            }

            db.Absence_Request_Reasons.Remove(absence_Request_Reason);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AbsenceRequestReason(5)/Absence_Request
        [EnableQuery]
        public IQueryable<Absence_Request> GetAbsence_Request([FromODataUri] int key)
        {
            return db.Absence_Request_Reasons.Where(m => m.ID == key).SelectMany(m => m.Absence_Request);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Absence_Request_ReasonExists(int key)
        {
            return db.Absence_Request_Reasons.Count(e => e.ID == key) > 0;
        }
    }
}
