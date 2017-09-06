using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

namespace Triad.CabinetOffice.Slipping.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
