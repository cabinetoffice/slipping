using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triad.CabinetOffice.Slipping.Web.ViewModels
{
    public class LocationAndHours
    {
            
        public int ID { get; set; }

        [Display(Name = "Location")]
        [Required]
        [RegularExpression("^(?=.*[a-zA-Z])([a-zA-Z0-9][,_@./#&+-]){1,20}$", ErrorMessage = "Location must be valid and must not exceed 20 characters")]
        public string Location { get; set; }

        [Display(Name = "hours")]
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Hours must be in whole number")]
        public int Hours { get; set; }
    }
}