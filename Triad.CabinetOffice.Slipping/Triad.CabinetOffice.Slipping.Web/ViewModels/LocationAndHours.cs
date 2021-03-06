﻿using System.ComponentModel.DataAnnotations;

namespace Triad.CabinetOffice.Slipping.Web.ViewModels
{
    public class LocationAndHours
    {
            
        public int ID { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location is required")]
        [RegularExpression("^(?=.*[a-zA-Z].*)([a-zA-Z0-9,_@./#&+\\s-]){1,100}$", ErrorMessage = "Location must be valid and must not exceed 100 characters")]
        public string Location { get; set; }

        [Display(Name = "hours")]
        //[Required(ErrorMessage = "Travel time to Westminster is required")]
        //[RegularExpression("^[0-9]{1,3}$", ErrorMessage = "Hours must be a whole number")]
        public string Hours { get; set; }
    }
}