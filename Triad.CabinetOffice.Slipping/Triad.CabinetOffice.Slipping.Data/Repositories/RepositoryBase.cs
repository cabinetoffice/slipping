using System.Linq;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;

namespace Triad.CabinetOffice.Slipping.Data.Repositories
{
    public class RepositoryBase
    {
        protected SlippingEntities db { get; set; }

        public RepositoryBase(SlippingEntities slippingContext)
        {
            this.db = slippingContext;
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
