using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Triad.CabinetOffice.Slipping.Web.ViewModels
{
    public class OppositionMPs
    {
        public int ID { get; set; }
        public bool? YesNo { get; set; }
        public Dictionary<int, string> MPs { get; set; }
        public OppositionMPs()
        {
        }
    }
}