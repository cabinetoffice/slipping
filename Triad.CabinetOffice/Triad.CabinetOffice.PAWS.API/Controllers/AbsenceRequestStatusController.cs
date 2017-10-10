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
    builder.EntitySet<Absence_Request_Status>("AbsenceRequestStatus");
    builder.EntitySet<Absence_Request>("Absence_Requests"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AbsenceRequestStatusController : ODataController
    {
        private PAWSEntities db = new PAWSEntities();

        // GET: odata/AbsenceRequestStatus
        [EnableQuery]
        public IQueryable<Absence_Request_Status> GetAbsenceRequestStatus()
        {
            return db.Absence_Request_Status;
        }

        // GET: odata/AbsenceRequestStatus(5)
        [EnableQuery]
        public SingleResult<Absence_Request_Status> GetAbsence_Request_Status([FromODataUri] int key)
        {
            return SingleResult.Create(db.Absence_Request_Status.Where(absence_Request_Status => absence_Request_Status.ID == key));
        }

        // PUT: odata/AbsenceRequestStatus(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Absence_Request_Status> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Absence_Request_Status absence_Request_Status = await db.Absence_Request_Status.FindAsync(key);
            if (absence_Request_Status == null)
            {
                return NotFound();
            }

            patch.Put(absence_Request_Status);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Absence_Request_StatusExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(absence_Request_Status);
        }

        // POST: odata/AbsenceRequestStatus
        public async Task<IHttpActionResult> Post(Absence_Request_Status absence_Request_Status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Absence_Request_Status.Add(absence_Request_Status);
            await db.SaveChangesAsync();

            return Created(absence_Request_Status);
        }

        // PATCH: odata/AbsenceRequestStatus(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Absence_Request_Status> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Absence_Request_Status absence_Request_Status = await db.Absence_Request_Status.FindAsync(key);
            if (absence_Request_Status == null)
            {
                return NotFound();
            }

            patch.Patch(absence_Request_Status);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Absence_Request_StatusExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(absence_Request_Status);
        }

        // DELETE: odata/AbsenceRequestStatus(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Absence_Request_Status absence_Request_Status = await db.Absence_Request_Status.FindAsync(key);
            if (absence_Request_Status == null)
            {
                return NotFound();
            }

            db.Absence_Request_Status.Remove(absence_Request_Status);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AbsenceRequestStatus(5)/Absence_Request
        [EnableQuery]
        public IQueryable<Absence_Request> GetAbsence_Request([FromODataUri] int key)
        {
            return db.Absence_Request_Status.Where(m => m.ID == key).SelectMany(m => m.Absence_Request);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Absence_Request_StatusExists(int key)
        {
            return db.Absence_Request_Status.Count(e => e.ID == key) > 0;
        }
    }
}
