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
        #region Properties

        private SlippingRepository Repository { get { return new SlippingRepository(); } }

        private int UserID
        {
            get
            {
                if (this.User.Identity is SlippingUserIdentity)
                {
                    return ((SlippingUserIdentity)this.User.Identity).ID;
                }
                else
                {
                    throw new Exception(string.Format("User.Identity is not expected type (expected SlippingUserIdentity but was {0})", this.User.Identity.GetType().Name));
                }
            }
        }

        #endregion Properties

        #region Methods

        private SlippingRequest Get(int requestId)
        {
            return Repository.Get(requestId, this.UserID);
        }

        private int CreateOrUpdate(SlippingRequest slippingRequest)
        {
            return Repository.CreateOrUpdate(slippingRequest, this.UserID);
        }

        #endregion Methods

        #region Action Methods

        // GET: Slipping
        public ActionResult Index()
        {
            return View();
        }

        // GET: Slipping/Create or Slipping/Edit/ID/FromDate
        [HttpGet]
        public ActionResult FromDate(int? id)
        {
            var model = new DateAndTime();
            if (id.HasValue)
            {
                SlippingRequest slippingRequest = Get(id.Value);
                model = new DateAndTime
                {
                    ID = slippingRequest.ID,
                    Date = slippingRequest.FromDate,
                    Hour = slippingRequest.FromDate.ToString("hh"),
                    Minute = slippingRequest.FromDate.ToString("mm")
                };
            }
            return View(model);
        }

        // POST: Slipping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FromDate(DateAndTime model)
        {
            if (ModelState.IsValid)
            {
                SlippingRequest slippingRequest = new SlippingRequest();
                slippingRequest.FromDate = model.GetDateTime();
                int id = CreateOrUpdate(slippingRequest);
                return RedirectToAction("ToDate", new { id = id });
            }
            else
            {
                return View(model);
            }
        }

        // GET: Slipping/Edit/ID/ToDate
        [HttpGet]
        public ActionResult ToDate(int id)
        {
            SlippingRequest slippingRequest = Get(id);

            if (slippingRequest != null)
            {
                var model = new DateAndTime
                {
                    ID = slippingRequest.ID,
                    Date = slippingRequest.ToDate.HasValue ? slippingRequest.ToDate.Value : slippingRequest.FromDate,
                    Hour = slippingRequest.ToDate.HasValue ? slippingRequest.ToDate.Value.ToString("hh") : slippingRequest.FromDate.ToString("hh"),
                    Minute = slippingRequest.ToDate.HasValue ? slippingRequest.ToDate.Value.ToString("mm") : slippingRequest.FromDate.ToString("mm")
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }

        // POST: Slipping/Edit/ID/ToDate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ToDate(int id, DateAndTime model)
        {
            SlippingRequest slippingRequest = Get(id);

            if (slippingRequest != null)
            {
                DateTime toDate = model.GetDateTime();

                if (toDate.Date < slippingRequest.FromDate.Date)
                {
                    ModelState.AddModelError("Date", "To Date cannot fall before From Date");
                }

                if (toDate.Date == slippingRequest.FromDate.Date)
                {
                    if (toDate.TimeOfDay <= slippingRequest.FromDate.TimeOfDay)
                    {
                        ModelState.AddModelError("Hour", "To Hour must be at least 15 minutes after From Hour");
                    }
                }

                if (ModelState.IsValid)
                {
                    slippingRequest.ToDate = model.GetDateTime();
                    CreateOrUpdate(slippingRequest);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("NotFound");
            }
        }

        #endregion Action Methods
    }
}