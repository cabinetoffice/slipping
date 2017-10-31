using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Triad.CabinetOffice.Slipping.Data.Models;

namespace Triad.CabinetOffice.Slipping.Web.ViewModels
{
    public class SlippingHistory
    {
        public string MPName { get; set; }

        public IEnumerable<SlipSummary> Slips { get; set; }
    }
}