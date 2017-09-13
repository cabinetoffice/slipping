using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;

namespace Triad.CabinetOffice.Slipping.Data.Models
{
    public class SlippingRequest
    {
        //similar to AbsenceRequest
        public int ID { get; set; }
        public Nullable<int> MPID { get; set; }
        public Nullable<int> ReasonID { get; set; }
        public string Details { get; set; }
        public int StatusID { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime? ToDate { get; set; }
        public string DecisionNotes { get; set; }
        public int CreatedBy { get; set; }
        public int LastChangedBy { get; set; }
        public bool? OppositionMPsAttending { get; set; }
        public Dictionary<int, string> OppositionMPs { get; set; }
    }
}
