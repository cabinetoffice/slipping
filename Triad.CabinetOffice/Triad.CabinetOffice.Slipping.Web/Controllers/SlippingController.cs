using System;
using System.Web.Mvc;
using Triad.CabinetOffice.Slipping.Data.Models;
using Triad.CabinetOffice.Slipping.Data.Repositories;
using Triad.CabinetOffice.Slipping.Web.Attributes;
using Triad.CabinetOffice.Slipping.Web.ViewModels;

namespace Triad.CabinetOffice.Slipping.Web.Controllers
{
    [SlippingAuthorize]
    public class SlippingController : Controller
    {
        //private SlippingEntities db = new SlippingEntities();

        private SlippingRepository Repository = new SlippingRepository();

        // GET: Slipping
        public ActionResult Index()
        {
            return View();
        }
        // GET: Slipping/FromDate/ID
        [HttpGet]
        public ActionResult FromDateAndTime(int? id)
        {
            var model = new DateAndTime();
            if (id.HasValue)
            {
                SlippingRequest slippingRequest = Get(id.Value);
                model = new DateAndTime
                {
                    ID = slippingRequest.ID,
                    Date = slippingRequest.FromDate,
                    Time = slippingRequest.FromDate.ToString("hh:mm")

                };
            }
            return View(model);
        }
        // GET: Slipping/ToDate/ID
        [HttpGet]
        public ActionResult ToDateAndTime(int id)
        {
            SlippingRequest slippingRequest = Get(id);
            var model = new DateAndTime
            {
                ID = slippingRequest.ID,
                Date = slippingRequest.ToDate.HasValue ? slippingRequest.ToDate.Value : slippingRequest.FromDate,
                Time = slippingRequest.ToDate.HasValue ? slippingRequest.ToDate.Value.ToString("hh:mm") : slippingRequest.FromDate.ToString("hh:mm")
            };
            return View(model);
        }
        // POST: Slipping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FromDateAndTime(DateAndTime model)
        {
            if (ModelState.IsValid)
            {
                SlippingRequest slippingRequest = new SlippingRequest();
                slippingRequest.FromDate = model.GetDateTime();
                slippingRequest.ID = model.ID;

                int id= CreateOrUpdate(slippingRequest);

                return RedirectToAction("ToDateAndTime",new { id= id });
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
        public ActionResult ToDateAndTime(int id, DateAndTime model)
        {

            if (ModelState.IsValid)
            {
                SlippingRequest slippingRequest = Get(id);
                slippingRequest.ToDate = model.GetDateTime();

                CreateOrUpdate(slippingRequest);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        private SlippingRequest Get(int id)
        {
            return Repository.Get(id);
        }
        private int  CreateOrUpdate(SlippingRequest slippingRequest)
        {
            //TODO: Pass User Id
            int id=Repository.CreateOrUpdate(slippingRequest, 1);

            return id;
        }
    }
}