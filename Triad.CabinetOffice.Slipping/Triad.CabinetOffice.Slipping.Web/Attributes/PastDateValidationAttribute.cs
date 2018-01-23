using System;
using System.ComponentModel.DataAnnotations;
using Triad.CabinetOffice.Slipping.Data.Extensions;

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
                return dateValue.Date >= DateTime.UtcNow.ToUkTimeFromUtc().Date;
            }
            else
            {
                return false;
            }
        }
    }
}