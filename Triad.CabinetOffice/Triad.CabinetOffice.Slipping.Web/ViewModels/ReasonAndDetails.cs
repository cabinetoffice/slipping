using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Triad.CabinetOffice.Slipping.Web.ViewModels
{
    public class ReasonAndDetails
    {
        public int ID { get; set; }

        //[Required]
        public string Details { get; set; }

        [Required]
        public string Reason { get; set; }

        public IEnumerable<SelectListItem> Reasons { get; set; }

    }
}