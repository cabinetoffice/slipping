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
    builder.EntitySet<Members_of_Parliament>("MemberOfParliament");
    builder.EntitySet<Absence_Request>("Absence_Requests"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MemberOfParliamentController : ODataController
    {
        private PAWSEntities db = new PAWSEntities();

        // GET: odata/MemberOfParliament
        [EnableQuery]
        public IQueryable<Members_of_Parliament> GetMemberOfParliament()
        {
            return db.Members_of_Parliaments;
        }

        // GET: odata/MemberOfParliament(5)
        [EnableQuery]
        public SingleResult<Members_of_Parliament> GetMembers_of_Parliament([FromODataUri] int key)
        {
            return SingleResult.Create(db.Members_of_Parliaments.Where(members_of_Parliament => members_of_Parliament.ID == key));
        }

        // PUT: odata/MemberOfParliament(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Members_of_Parliament> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Members_of_Parliament members_of_Parliament = await db.Members_of_Parliaments.FindAsync(key);
            if (members_of_Parliament == null)
            {
                return NotFound();
            }

            patch.Put(members_of_Parliament);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Members_of_ParliamentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(members_of_Parliament);
        }

        // POST: odata/MemberOfParliament
        public async Task<IHttpActionResult> Post(Members_of_Parliament members_of_Parliament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Members_of_Parliaments.Add(members_of_Parliament);
            await db.SaveChangesAsync();

            return Created(members_of_Parliament);
        }

        // PATCH: odata/MemberOfParliament(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Members_of_Parliament> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Members_of_Parliament members_of_Parliament = await db.Members_of_Parliaments.FindAsync(key);
            if (members_of_Parliament == null)
            {
                return NotFound();
            }

            patch.Patch(members_of_Parliament);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Members_of_ParliamentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(members_of_Parliament);
        }

        // DELETE: odata/MemberOfParliament(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Members_of_Parliament members_of_Parliament = await db.Members_of_Parliaments.FindAsync(key);
            if (members_of_Parliament == null)
            {
                return NotFound();
            }

            db.Members_of_Parliaments.Remove(members_of_Parliament);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/MemberOfParliament(5)/Absence_Requests
        [EnableQuery]
        public IQueryable<Absence_Request> GetAbsence_Requests([FromODataUri] int key)
        {
            return db.Members_of_Parliaments.Where(m => m.ID == key).SelectMany(m => m.Absence_Requests);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Members_of_ParliamentExists(int key)
        {
            return db.Members_of_Parliaments.Count(e => e.ID == key) > 0;
        }
    }
}
