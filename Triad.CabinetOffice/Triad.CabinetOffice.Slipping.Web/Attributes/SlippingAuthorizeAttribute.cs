using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;
using Triad.CabinetOffice.Slipping.Data.Repositories;
using Triad.CabinetOffice.Slipping.Web.ViewModels;

namespace Triad.CabinetOffice.Slipping.Web.Attributes
{
    public class SlippingAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (base.AuthorizeCore(httpContext))
            {
                string username = httpContext.User.Identity.Name;
                UserRepository repository = new UserRepository();
                User user = repository.GetByUsername(username);
                httpContext.Session["MPID"] = user.UserMPs1.First().MPID;

                if (user != null)
                {
                    httpContext.User = new SlippingUserPrincipal()
                    {
                        Identity = new SlippingUserIdentity()
                        {
                            AuthenticationType = httpContext.User.Identity.AuthenticationType,
                            ID = user.ID,
                            IsAuthenticated = httpContext.User.Identity.IsAuthenticated,
                            Name = user.Username,
                        }
                    };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //User is logged in but has no access
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Home", action = "Index" })
                );
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}