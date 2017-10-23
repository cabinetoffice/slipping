using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;

namespace Triad.CabinetOffice.PAWS.API.Controllers
{
    [Authorize]
    public class SessionsController : ODataController
    {
        private SlippingEntities db = new SlippingEntities();

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
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Session> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Session session = await db.Sessions.FindAsync(key);
            if (session == null)
            {
                return NotFound();
            }

            patch.Put(session);

            try
            {
                await db.SaveChangesAsync();
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
        public async Task<IHttpActionResult> Post(Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sessions.Add(session);
            await db.SaveChangesAsync();

            return Created(session);
        }

        // PATCH: odata/Sessions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Session> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Session session = await db.Sessions.FindAsync(key);
            if (session == null)
            {
                return NotFound();
            }

            patch.Patch(session);

            try
            {
                await db.SaveChangesAsync();
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
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Session session = await db.Sessions.FindAsync(key);
            if (session == null)
            {
                return NotFound();
            }

            db.Sessions.Remove(session);
            await db.SaveChangesAsync();

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
