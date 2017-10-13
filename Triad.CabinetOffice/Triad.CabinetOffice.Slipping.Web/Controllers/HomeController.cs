using System.Configuration;
using System.Web.Mvc;

namespace Triad.CabinetOffice.Slipping.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect(ConfigurationManager.AppSettings["StartPageURL"]);
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}