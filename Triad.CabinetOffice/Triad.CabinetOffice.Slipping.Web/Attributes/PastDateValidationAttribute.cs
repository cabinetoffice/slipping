using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Triad.CabinetOffice.Slipping.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PastDateValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime)
            {
                DateTime dateValue = Convert.ToDateTime(value);
                return dateValue > DateTime.Now.Date;
            }
            else
            {
                return false;
            }
        }
    }
}