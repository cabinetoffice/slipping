using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Triad.CabinetOffice.Slipping.Web.ViewModels
{
    public class DeletedSlippingRequest
    {
        public DateTime Date {get;set;}
        public DeletedSlippingRequest(DateTime date)
        {
            this.Date = date;
        }
    }
}