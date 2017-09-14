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
    }
}
