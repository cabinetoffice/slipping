using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triad.CabinetOffice.Slipping.Data.Models
{
    public class OppositionMP
    {
        public int ID { get; set; }
        public int? MPID { get; set; }
        [Required(ErrorMessage = "Opposition MP full name is required")]
        [Display(Name = "Full name")]
        public string FullName { get; set; }
    }
}
