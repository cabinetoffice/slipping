using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triad.CabinetOffice.Slipping.Data.Models;

namespace Triad.CabinetOffice.Slipping.Web.ViewModels
{
    public class ReasonAndDetails
    {
        public int ID { get; set; }

        [RegularExpression("^\\W*(?:\\w+\\b\\W*){1,200}$", ErrorMessage = "Detail must be valid and must not exceed 200 words")]
        public string Details1 { get; set; }
        [RegularExpression("^\\W*(?:\\w+\\b\\W*){1,200}$", ErrorMessage = "Detail must be valid and must not exceed 200 words")]
        public string Details2 { get; set; }
        [RegularExpression("^\\W*(?:\\w+\\b\\W*){1,200}$", ErrorMessage = "Detail must be valid and must not exceed 200 words")]
        public string Details3 { get; set; }
        [RegularExpression("^\\W*(?:\\w+\\b\\W*){1,100}$", ErrorMessage = "Detail must be valid and must not exceed 100 words")]
        public string Details5 { get; set; }
        
        [Required]
        public string Reason { get; set; }

        public ICollection<RequestReason> Reasons { get; set; }

    }
}