using System.Configuration;
using System.Web.Mvc;

namespace Triad.CabinetOffice.SlippingPublic.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}