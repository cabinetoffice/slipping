using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;

namespace Triad.CabinetOffice.Slipping.Web.Controllers
{
    [Authorize]
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
            return View();
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
        public ActionResult FromDateAndTime([Bind(Include = "FromDate")] AbsenceRequest absenceRequest)
        {
            absenceRequest.CreatedDate = DateTime.Now;
            absenceRequest.CreatedBy = 1;
            absenceRequest.StatusID = 1;
            absenceRequest.LastChangedDate = DateTime.Now;
            absenceRequest.LastChangedBy = 1;
            absenceRequest.ToDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.AbsenceRequests.Add(absenceRequest);
                db.SaveChanges();
                return RedirectToAction("ToDateAndTime");
            }

            return View(absenceRequest);
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