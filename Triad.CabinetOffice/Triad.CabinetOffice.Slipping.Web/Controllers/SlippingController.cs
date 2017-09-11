using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;
using Triad.CabinetOffice.Slipping.Web.Attributes;
using Triad.CabinetOffice.Slipping.Web.ViewModels;

namespace Triad.CabinetOffice.Slipping.Web.Controllers
{
    [SlippingAuthorize]
    public class SlippingController : Controller
    {
        private SlippingEntities db = new SlippingEntities();

        // GET: Slipping
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult FromDateAndTime()
        {
            var model = new FromDateAndTime();
            return View(model);
        }

        public ActionResult ToDateAndTime()
        {
            return View();
        }

        // POST: Slipping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FromDateAndTime(FromDateAndTime model)
        {
            if (ModelState.IsValid)
            {
                AbsenceRequest absenceRequest = new AbsenceRequest();
                absenceRequest.CreatedDate = DateTime.Now;
                absenceRequest.CreatedBy = 1;
                absenceRequest.StatusID = 1;
                absenceRequest.LastChangedDate = DateTime.Now;
                absenceRequest.LastChangedBy = 1;

                absenceRequest.FromDate = model.GetFromDateTime();
                absenceRequest.ToDate = DateTime.Now;

                db.AbsenceRequests.Add(absenceRequest);
                db.SaveChanges();
                return RedirectToAction("ToDateAndTime");
            }
            else
            {
                return View(model);
            }
        }

        // POST: Slipping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ToDateAndTime([Bind(Include = "ToDate")] AbsenceRequest absenceRequest)
        {

            if (ModelState.IsValid)
            {
                db.AbsenceRequests.Add(absenceRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(absenceRequest);
        }
    }
}