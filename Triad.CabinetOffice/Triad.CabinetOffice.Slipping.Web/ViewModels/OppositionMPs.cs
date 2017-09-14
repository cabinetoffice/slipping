using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Triad.CabinetOffice.Slipping.Data.Models;

namespace Triad.CabinetOffice.Slipping.Web.ViewModels
{
    public class OppositionMPs
    {
        public int ID { get; set; }
        [Required]
        public bool? YesNo { get; set; }
        public List<OppositionMP> MPs { get; set; }
        public OppositionMPs()
        {
        }
    }
}