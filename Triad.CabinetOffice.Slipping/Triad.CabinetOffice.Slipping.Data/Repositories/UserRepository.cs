using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;

namespace Triad.CabinetOffice.Slipping.Data.Repositories
{
    public class UserRepository : RepositoryBase
    {
        public UserRepository() : base()
        {
        }

        public UserRepository(SlippingEntities entities) : base(entities)
        {
        }

        public User GetByUsername(string username)
        {
            int userCount = this.db.Users.Count(u => u.Username == username);

            if (userCount == 1)
            {
                return this.db.Users.Single(u => u.Username == username);
            }
            else
            {
                return null;
            }
        }
    }
}
