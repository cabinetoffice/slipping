using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.PAWS2;

namespace Triad.CabinetOffice.PAWS.API.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Triad.CabinetOffice.Slipping.Data.EntityFramework.PAWS2;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Party>("Parties");
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PartiesController : ODataController
    {
        private PAWS2Entities db = new PAWS2Entities();

        // GET: odata/Parties
        [EnableQuery]
        public IQueryable<Party> GetParties()
        {
            return db.Parties;
        }

        // GET: odata/Parties(5)
        [EnableQuery]
        public SingleResult<Party> GetParty([FromODataUri] int key)
        {
            return SingleResult.Create(db.Parties.Where(party => party.ID == key));
        }

        // PUT: odata/Parties(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Party> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Party party = db.Parties.Find(key);
            if (party == null)
            {
                return NotFound();
            }

            patch.Put(party);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(party);
        }

        // POST: odata/Parties
        public IHttpActionResult Post(Party party)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parties.Add(party);
            db.SaveChanges();

            return Created(party);
        }

        // PATCH: odata/Parties(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Party> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Party party = db.Parties.Find(key);
            if (party == null)
            {
                return NotFound();
            }

            patch.Patch(party);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(party);
        }

        // DELETE: odata/Parties(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Party party = db.Parties.Find(key);
            if (party == null)
            {
                return NotFound();
            }

            db.Parties.Remove(party);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Parties(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.Parties.Where(m => m.ID == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PartyExists(int key)
        {
            return db.Parties.Count(e => e.ID == key) > 0;
        }
    }
}
