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
    builder.EntitySet<Session>("Sessions");
    builder.EntitySet<Division>("Divisions"); 
    builder.EntitySet<User>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SessionsController : ODataController
    {
        private PAWS2Entities db = new PAWS2Entities();

        // GET: odata/Sessions
        [EnableQuery]
        public IQueryable<Session> GetSessions()
        {
            return db.Sessions;
        }

        // GET: odata/Sessions(5)
        [EnableQuery]
        public SingleResult<Session> GetSession([FromODataUri] int key)
        {
            return SingleResult.Create(db.Sessions.Where(session => session.ID == key));
        }

        // PUT: odata/Sessions(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Session> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Session session = db.Sessions.Find(key);
            if (session == null)
            {
                return NotFound();
            }

            patch.Put(session);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(session);
        }

        // POST: odata/Sessions
        public IHttpActionResult Post(Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sessions.Add(session);
            db.SaveChanges();

            return Created(session);
        }

        // PATCH: odata/Sessions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Session> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Session session = db.Sessions.Find(key);
            if (session == null)
            {
                return NotFound();
            }

            patch.Patch(session);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(session);
        }

        // DELETE: odata/Sessions(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Session session = db.Sessions.Find(key);
            if (session == null)
            {
                return NotFound();
            }

            db.Sessions.Remove(session);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Sessions(5)/Divisions
        [EnableQuery]
        public IQueryable<Division> GetDivisions([FromODataUri] int key)
        {
            return db.Sessions.Where(m => m.ID == key).SelectMany(m => m.Divisions);
        }

        // GET: odata/Sessions(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.Sessions.Where(m => m.ID == key).Select(m => m.User));
        }

        // GET: odata/Sessions(5)/User1
        [EnableQuery]
        public SingleResult<User> GetUser1([FromODataUri] int key)
        {
            return SingleResult.Create(db.Sessions.Where(m => m.ID == key).Select(m => m.User1));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SessionExists(int key)
        {
            return db.Sessions.Count(e => e.ID == key) > 0;
        }
    }
}
