using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triad.CabinetOffice.Slipping.Web.Attributes;

namespace Triad.CabinetOffice.Slipping.Web.ViewModels
{
    public class DateAndTime
    {
        public int ID { get; set; }

        [Display(Name = "Date")]
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Date must be in format dd/mm/yyyy")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [PastDateValidation(ErrorMessage = "Date cannot be in the past")]
        [FutureDateValidation(ErrorMessage = "Date can be no more than 5 years in the future")]
        public DateTime Date { get; set; }

        [Display(Name = "Hour")]
        [Required]
        [RegularExpression("^[0-2][0-9]$", ErrorMessage = "Time must be in format hh")]
        public string Hour { get; set; }

        [Display(Name = "Minute")]
        [Required]
        [RegularExpression("^[0-5][0-9]$", ErrorMessage = "Minute must be in format mm")]
        public string Minute { get; set; }

        public IEnumerable<SelectListItem> Hours = new List<SelectListItem>()
        {
            new SelectListItem(){ Text="00", Value="00"},
            new SelectListItem(){ Text="01", Value="01"},
            new SelectListItem(){ Text="02", Value="02"},
            new SelectListItem(){ Text="03", Value="03"},
            new SelectListItem(){ Text="04", Value="04"},
            new SelectListItem(){ Text="05", Value="05"},
            new SelectListItem(){ Text="06", Value="06"},
            new SelectListItem(){ Text="07", Value="07"},
            new SelectListItem(){ Text="08", Value="08"},
            new SelectListItem(){ Text="09", Value="09"},
            new SelectListItem(){ Text="10", Value="10"},
            new SelectListItem(){ Text="11", Value="11"},
            new SelectListItem(){ Text="12", Value="12"},
            new SelectListItem(){ Text="13", Value="13"},
            new SelectListItem(){ Text="14", Value="14"},
            new SelectListItem(){ Text="15", Value="15"},
            new SelectListItem(){ Text="16", Value="16"},
            new SelectListItem(){ Text="17", Value="17"},
            new SelectListItem(){ Text="18", Value="18"},
            new SelectListItem(){ Text="19", Value="19"},
            new SelectListItem(){ Text="20", Value="20"},
            new SelectListItem(){ Text="21", Value="21"},
            new SelectListItem(){ Text="22", Value="22"},
            new SelectListItem(){ Text="23", Value="23"}
        };

        public IEnumerable<SelectListItem> Minutes = new List<SelectListItem>()
        {
            new SelectListItem(){Text="00",Value="00" },
            new SelectListItem(){Text="15",Value="15" },
            new SelectListItem(){Text="30",Value="30" },
            new SelectListItem(){Text="45",Value="45" }
        };

        public DateAndTime()
        {
            this.Date = DateTime.Now;
            this.Hour = "00";
            this.Minute = "00";
        }

        public DateTime GetDateTime()
        {
            DateTime result = this.Date;

            if (!string.IsNullOrEmpty(this.Hour))
            {
                int hours;
                if (int.TryParse(this.Hour, out hours))
                {
                    result = result.AddHours(hours);
                }
            }

            if (!string.IsNullOrEmpty(this.Minute))
            {
                int minutes;
                if (int.TryParse(this.Minute, out minutes))
                {
                    result = result.AddMinutes(minutes);
                }
            }

            return result;
        }
    }
}