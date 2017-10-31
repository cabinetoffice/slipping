using System;

namespace Triad.CabinetOffice.Slipping.Web.Exceptions
{
    public class SlippingException : Exception
    {
        public SlippingException() { }
        public SlippingException(string message) : base(message) { }
        public SlippingException(string message, Exception inner) : base(message, inner) { }

    }
}