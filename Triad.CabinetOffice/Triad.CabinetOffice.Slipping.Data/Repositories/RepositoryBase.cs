using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.PAWS;

namespace Triad.CabinetOffice.Slipping.Data.Repositories
{
    public class RepositoryBase
    {
        protected SlippingEntities db { get; set; }
        protected PAWSEntities PAWSDB { get; set; }

        public RepositoryBase(SlippingEntities slippingContext, PAWSEntities pawsContext)
        {
            this.db = slippingContext;
            this.PAWSDB = pawsContext;
        }

        public RepositoryBase(SlippingEntities context) : this(context, new PAWSEntities())
        {
        }

        public RepositoryBase() : this(new SlippingEntities())
        {
        }

        protected bool UserCanActForMP(int userId, int MPID)
        {
            // Check that the Absence Request belongs to an MP that the user can edit
            return db.UserMPs.Count(ump => ump.UserID == userId && ump.MPID == MPID) > 0;
        }
    }
}
