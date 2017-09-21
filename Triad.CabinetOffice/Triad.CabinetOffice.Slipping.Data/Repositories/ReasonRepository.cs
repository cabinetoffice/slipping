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
            requestReason = new List<RequestReason>();
            requestReason.Add(new RequestReason { ID = 1, Reason = "Government Work (Secretaries of State / Ministers of State only)" });
            requestReason.Add(new RequestReason { ID = 2, Reason = "Constituency Engagement" });
            requestReason.Add(new RequestReason { ID = 3, Reason = "Parliamentary Campaigning Activity" });
            requestReason.Add(new RequestReason { ID = 4, Reason = "Personal/Other" });
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
