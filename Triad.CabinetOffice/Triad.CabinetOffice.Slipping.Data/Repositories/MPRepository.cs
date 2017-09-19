using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.PAWS;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;
using Triad.CabinetOffice.Slipping.Data.Models;

namespace Triad.CabinetOffice.Slipping.Data.Repositories
{
    public class MPRepository : RepositoryBase
    {
        public MPRepository() : base()
        {
        }

        public MPRepository(SlippingEntities slippingContext, PAWSEntities pawsContext) : base(slippingContext, pawsContext)
        {
        }

        public MP Get(int MPID, int userId)
        {
            if (UserCanActForMP(userId, MPID))
            {
                Members_of_Parliament mp = this.PAWSDB.Members_of_Parliaments.Find(MPID);

                if (mp != null)
                {
                    return new MP()
                    {
                        Name = mp.Full_Name,
                        EmailAddress = mp.Email_Address
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
