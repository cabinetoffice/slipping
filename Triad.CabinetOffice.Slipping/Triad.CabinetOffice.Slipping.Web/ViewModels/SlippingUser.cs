using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Triad.CabinetOffice.Slipping.Web.ViewModels
{
    public class SlippingUserIdentity : IIdentity
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }
    }

    public class SlippingUserPrincipal : IPrincipal
    {
        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}