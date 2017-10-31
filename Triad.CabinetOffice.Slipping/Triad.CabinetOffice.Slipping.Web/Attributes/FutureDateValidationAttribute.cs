using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Triad.CabinetOffice.Slipping.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FutureDateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime)
            {
                DateTime dateValue = Convert.ToDateTime(value);
                return dateValue < DateTime.Now.AddYears(5).Date;
            }
            else
            {
                return false;
            }
        }
    }
}