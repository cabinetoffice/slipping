using System.Configuration;
using System.Web.Mvc;

namespace Triad.CabinetOffice.Slipping.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
            //return Redirect(ConfigurationManager.AppSettings["StartPageURL"]);
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}