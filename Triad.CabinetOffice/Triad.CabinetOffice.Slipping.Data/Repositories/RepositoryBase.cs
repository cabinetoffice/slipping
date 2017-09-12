using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;

namespace Triad.CabinetOffice.Slipping.Data.Repositories
{
    public class RepositoryBase
    {
        protected SlippingEntities db { get; set; }

        public RepositoryBase(SlippingEntities context)
        {
            this.db = context;
        }

        public RepositoryBase() : this(new SlippingEntities())
        {
        }
    }
}
