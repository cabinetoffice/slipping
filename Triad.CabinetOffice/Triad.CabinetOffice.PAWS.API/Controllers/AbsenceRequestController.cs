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
    builder.EntitySet<Absence_Request>("AbsenceRequest");
    builder.EntitySet<Absence_Request_Status>("Absence_Request_Status"); 
    builder.EntitySet<Absence_Request_Reason>("Absence_Request_Reasons"); 
    builder.EntitySet<Members_of_Parliament>("Members_of_Parliaments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AbsenceRequestController : ODataController
    {
        private PAWSEntities db = new PAWSEntities();

        // GET: odata/AbsenceRequest
        [EnableQuery]
        public IQueryable<Absence_Request> GetAbsenceRequest()
        {
            return db.Absence_Requests;
        }

        // GET: odata/AbsenceRequest(5)
        [EnableQuery]
        public SingleResult<Absence_Request> GetAbsence_Request([FromODataUri] int key)
        {
            return SingleResult.Create(db.Absence_Requests.Where(absence_Request => absence_Request.ID == key));
        }

        // PUT: odata/AbsenceRequest(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Absence_Request> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Absence_Request absence_Request = await db.Absence_Requests.FindAsync(key);
            if (absence_Request == null)
            {
                return NotFound();
            }

            patch.Put(absence_Request);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Absence_RequestExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(absence_Request);
        }

        // POST: odata/AbsenceRequest
        public async Task<IHttpActionResult> Post(Absence_Request absence_Request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Absence_Requests.Add(absence_Request);
            await db.SaveChangesAsync();

            return Created(absence_Request);
        }

        // PATCH: odata/AbsenceRequest(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Absence_Request> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Absence_Request absence_Request = await db.Absence_Requests.FindAsync(key);
            if (absence_Request == null)
            {
                return NotFound();
            }

            patch.Patch(absence_Request);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Absence_RequestExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(absence_Request);
        }

        // DELETE: odata/AbsenceRequest(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Absence_Request absence_Request = await db.Absence_Requests.FindAsync(key);
            if (absence_Request == null)
            {
                return NotFound();
            }

            db.Absence_Requests.Remove(absence_Request);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AbsenceRequest(5)/Absence_Request_Status
        [EnableQuery]
        public SingleResult<Absence_Request_Status> GetAbsence_Request_Status([FromODataUri] int key)
        {
            return SingleResult.Create(db.Absence_Requests.Where(m => m.ID == key).Select(m => m.Absence_Request_Status));
        }

        // GET: odata/AbsenceRequest(5)/Absence_Request_Reason
        [EnableQuery]
        public SingleResult<Absence_Request_Reason> GetAbsence_Request_Reason([FromODataUri] int key)
        {
            return SingleResult.Create(db.Absence_Requests.Where(m => m.ID == key).Select(m => m.Absence_Request_Reason));
        }

        // GET: odata/AbsenceRequest(5)/Member_of_Parliament
        [EnableQuery]
        public SingleResult<Members_of_Parliament> GetMember_of_Parliament([FromODataUri] int key)
        {
            return SingleResult.Create(db.Absence_Requests.Where(m => m.ID == key).Select(m => m.Member_of_Parliament));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Absence_RequestExists(int key)
        {
            return db.Absence_Requests.Count(e => e.ID == key) > 0;
        }
    }
}
