﻿using System.Web.Mvc;

namespace Triad.CabinetOffice.Slipping.Web.Controllers
{
    public class HelpController : Controller
    {
        // GET: Help
        public ActionResult Index()
        {
            return View("Cookies");
        }

        // GET: Cookies
        public ActionResult Cookies()
        {
            return View();
        }
    }
}