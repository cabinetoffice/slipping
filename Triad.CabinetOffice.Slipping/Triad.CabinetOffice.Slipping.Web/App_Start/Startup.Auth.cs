using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Threading.Tasks;

namespace Triad.CabinetOffice.Slipping.Web
{
    public partial class Startup
    {
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        private static string authority = aadInstance + tenantId;

        private static string AppGatewayHostName
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains("AppGatewayHostName"))
                {
                    return ConfigurationManager.AppSettings["AppGatewayHostName"];
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            var options = new OpenIdConnectAuthenticationOptions
            {
                ClientId = clientId,
                Authority = authority,
                PostLogoutRedirectUri = postLogoutRedirectUri,
            };

            if (!string.IsNullOrEmpty(AppGatewayHostName))
            {
                options.Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    SecurityTokenValidated = async ctx =>
                    {
                        // The default RedirectUri on the Authentication Ticket uses the host name of the App Service, e.g. slipping-dev.azurewebsites.net
                        // We need to change the RedirectUri to use the host name for the App Gateway, e.g. slipping.cabinetoffice.gov.uk
                        Uri uri = new Uri(ctx.AuthenticationTicket.Properties.RedirectUri);
                        ctx.AuthenticationTicket.Properties.RedirectUri = string.Format("{0}{1}", AppGatewayHostName, uri.AbsolutePath);
                    }
                };
            }

            app.UseOpenIdConnectAuthentication(options);
        }
    }
}
