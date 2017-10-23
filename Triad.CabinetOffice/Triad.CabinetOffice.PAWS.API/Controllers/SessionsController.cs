using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Triad.CabinetOffice.PAWS.API.Models;
using Triad.CabinetOffice.PAWS.API.Repositories;
using Microsoft.Data.OData;

namespace Triad.CabinetOffice.PAWS.API.Controllers
{
    [Authorize]
    public class SessionsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings()
        {
            AllowedFunctions = AllowedFunctions.None,
            AllowedLogicalOperators = AllowedLogicalOperators.Equal,
            AllowedArithmeticOperators = AllowedArithmeticOperators.None,
            AllowedQueryOptions = AllowedQueryOptions.Filter
        };
        
        private IRepository<Session> repository = new SessionRepository();

        // GET: odata/Sessions
        public IHttpActionResult GetSessions(ODataQueryOptions<Session> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
                return Ok(repository.List());

            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: odata/Sessions(5)
        public IHttpActionResult GetSession([FromODataUri] int key, ODataQueryOptions<Session> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
                return Ok(repository.Get(key));
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

         }

        // PUT: odata/Sessions(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Session> session)
        {
            Validate(session.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                // TODO: Get the entity here.

                // delta.Put(session);

                // TODO: Save the patched entity.
                repository.Update(session.GetEntity());

                return Ok(session.GetEntity());
            }


            
        }

        // POST: odata/Sessions
        public IHttpActionResult Post(Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                // TODO: Add create logic here.
                repository.Add(session);
                return Ok(session);
            }

            

        }

        // PATCH: odata/Sessions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Session> session)
        {
            Validate(session.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                repository.Update(session.GetEntity());

                return Ok(session.GetEntity());
                // TODO: Get the entity here.

                // delta.Patch(session);

                // TODO: Save the patched entity.
            }



        }

        // DELETE: odata/Sessions(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            // TODO: Add delete logic here.

            repository.Delete(repository.Get(key));

             return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
