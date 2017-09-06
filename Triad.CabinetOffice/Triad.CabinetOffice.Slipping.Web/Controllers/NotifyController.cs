using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notify.Client;
using Notify.Models;
using Notify.Models.Responses;

namespace Triad.CabinetOffice.Slipping.Web.Controllers
{
    public class NotifyController : Controller
    {
        // GET: Notify
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection formValues)
        {
            string apiKey = "slippingdev-19c65c29-9cb4-49cd-8be3-38fed8f884be-c0e20c1f-3fab-4991-bfe5-da90288a2338";
            NotificationClient client = new NotificationClient(apiKey);
            string emailAddress = "david.preston@triad.co.uk";
            string templateId = "b78dfef1-10fc-48f9-afa3-947cac2499d5";
            //Template missing personalisation: name, absence_date, reference
            Dictionary<string, dynamic> personalisations = new Dictionary<string, dynamic>()
            {
                { "name", "David Preston" },
                { "absence_date", new DateTime(2017,10,14).ToShortDateString() },
                { "reference", "1234" }
            };
            EmailNotificationResponse response = client.SendEmail(emailAddress, templateId, personalisations);
            return View();
        }
    }
}