using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;

namespace Triad.CabinetOffice.Slipping.Data.Repositories
{
    public class HealthCheckRepository : RepositoryBase
    {
        public HealthCheckRepository() : base()
        {
        }

        public HealthCheckRepository(SlippingEntities slippingContext) : base(slippingContext)
        {
        }
        
        /// <summary>
        /// Check we can access the database 
        /// (just read the first row from the MembersOfParliament table)
        /// </summary>
        /// <returns>true if database is accessible</returns>
        public bool HealthCheck()
        {
            bool dbAccessible = false;
            try
            {
                MembersOfParliament mp = db.MembersOfParliaments.FirstOrDefault();
                dbAccessible = true;
            }
            catch (Exception)
            {
                throw;
            }
            return dbAccessible;
        }
    }
}
