using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triad.CabinetOffice.Slipping.Data.Models;

namespace Triad.CabinetOffice.Slipping.Data.Repositories
{
    public class ReasonRepository: RepositoryBase
    {
        private ICollection<RequestReason> requestReason;

        public ReasonRepository() : base()
        {
            requestReason = PAWSDB.Absence_Request_Reasons.Select(arr => new RequestReason() { ID = arr.ID, Reason = arr.Reason }).ToList();
        }

        public ICollection<RequestReason> Get()
        {
            return requestReason;
        }

        public RequestReason Get(int ID)
        {
            return requestReason.Where(r => r.ID == ID).FirstOrDefault(); 
        }
    }
}
