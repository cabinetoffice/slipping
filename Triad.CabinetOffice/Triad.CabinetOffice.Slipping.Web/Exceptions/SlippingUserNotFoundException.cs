using System;

namespace Triad.CabinetOffice.Slipping.Web.Exceptions
{
    public class SlippingUserNotFoundException : SlippingException
    {
        public SlippingUserNotFoundException() { }
        public SlippingUserNotFoundException(string message) : base(message) { }
        public SlippingUserNotFoundException(string message, Exception inner) : base(message, inner) { }

    }
}