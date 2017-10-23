using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Triad.CabinetOffice.PAWS.API.Models
{
    public class Session
    {
        [Key]
        public int ID { get; set; }
        public int SessionTitle { get; set; }
        public int ToDate { get; set; }
    }
}