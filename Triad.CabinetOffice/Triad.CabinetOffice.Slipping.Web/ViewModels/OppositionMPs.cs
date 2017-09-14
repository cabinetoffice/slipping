using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Triad.CabinetOffice.Slipping.Web.ViewModels
{
    public class OppositionMPs
    {
        [Required]
        public bool? YesNo { get; set; }
        public Dictionary<int,string> MPs { get; set; }
        public OppositionMPs()
        {
        }
    }
}