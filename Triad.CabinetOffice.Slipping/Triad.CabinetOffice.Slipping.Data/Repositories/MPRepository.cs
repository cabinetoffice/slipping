using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;
using Triad.CabinetOffice.Slipping.Data.Models;

namespace Triad.CabinetOffice.Slipping.Data.Repositories
{
    public class MPRepository : RepositoryBase
    {
        public MPRepository() : base()
        {
        }

        public MPRepository(SlippingEntities slippingContext) : base(slippingContext)
        {
        }

        public MP Get(int MPID, int userId)
        {
            if (UserCanActForMP(userId, MPID))
            {
                var mp = db.MembersOfParliaments.Find(MPID);

                if (mp != null)
                {
                    return new MP()
                    {
                        Name = mp.FullName,
                        EmailAddress = mp.EmailAddress
                    };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
