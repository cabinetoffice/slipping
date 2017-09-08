using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triad.CabinetOffice.Slipping.Web.Models;

namespace Triad.CabinetOffice.Slipping.Web.Controllers
{
    [Authorize]
    public class SlippingController : Controller
    {
        private PAWS2Entities db = new PAWS2Entities();

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
        public ActionResult FromDateAndTime([Bind(Include = "From_Time,From_Date")] Absence_Request absence_Request)
        {
            absence_Request.Govt_MP = 1;
            absence_Request.Date_Created = DateTime.Now;
            absence_Request.Status = 1;


            if (ModelState.IsValid)
            {
                db.Absence_Request.Add(absence_Request);
                db.SaveChanges();
                return RedirectToAction("ToDateAndTime");
            }

            ViewBag.Reason = new SelectList(db.Absence_Request_Reason, "ID", "Reason", absence_Request.Reason);
            ViewBag.Status = new SelectList(db.Absence_Request_Status, "ID", "Status", absence_Request.Status);
            return View(absence_Request);
        }

        // POST: Slipping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ToDateAndTime([Bind(Include = "To_Time,To_Date")] Absence_Request absence_Request)
        {
            absence_Request.Govt_MP = 1;
            absence_Request.Date_Created = DateTime.Now;
            absence_Request.Status = 1;


            if (ModelState.IsValid)
            {
                db.Absence_Request.Add(absence_Request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Reason = new SelectList(db.Absence_Request_Reason, "ID", "Reason", absence_Request.Reason);
            ViewBag.Status = new SelectList(db.Absence_Request_Status, "ID", "Status", absence_Request.Status);
            return View(absence_Request);
        }
    }
}