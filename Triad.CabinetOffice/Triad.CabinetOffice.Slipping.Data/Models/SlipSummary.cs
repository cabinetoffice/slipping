using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triad.CabinetOffice.Slipping.Data.Models
{
    public class SlipSummary
    {
        public int ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Status { get; set; }
        public bool IsUnsubmitted { get; set; }
        public int MPID { get; set; }
        public string Location { get; set; }
        public int TravelTimeInHours { get; set; }
        public string Reason { get; set; }
        public string Details { get; set; }
        public bool OppositionMPsAttending { get; set; }
        public List<OppositionMP> OppositionMPs { get; set; }
    }
}
