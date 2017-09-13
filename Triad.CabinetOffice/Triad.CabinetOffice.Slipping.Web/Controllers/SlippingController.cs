using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
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

        private SlippingRepository SlippingRepository { get { return new SlippingRepository(); } }

        private MPRepository MPRepository { get { return new MPRepository(); } }

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

        private int MPID { get { return Convert.ToInt32(Session["MPID"]); } }

        #endregion Properties

        #region Methods

        private SlippingRequest Get(int requestId)
        {
            return SlippingRepository.Get(requestId, this.UserID);
        }

        private int CreateOrUpdate(SlippingRequest slippingRequest)
        {
            return SlippingRepository.CreateOrUpdate(slippingRequest, this.MPID, this.UserID);
        }

        #endregion Methods

        #region Action Methods

        // GET: Slipping
        public ActionResult Index(bool viewAll = false)
        {
            MP mp = MPRepository.Get(this.MPID, this.UserID);
            int initialSlippingRequestListLength = Convert.ToInt32(WebConfigurationManager.AppSettings["InitialSlippingRequestListLength"]);
            IEnumerable<SlipSummary> slips = this.SlippingRepository.GetSummaries(this.MPID, this.UserID);

            IEnumerable<SlipSummary> visibleSlips = slips
                .Where(s => s.ToDate.Date >= DateTime.Now.Date)
                .OrderBy(s => s.ToDate);
            ViewBag.ShowViewAll = visibleSlips.Count() > initialSlippingRequestListLength && !viewAll;

            SlippingHistory model = new SlippingHistory()
            {
                MPName = mp != null ? mp.Name : "Unknown",
                Slips = viewAll ? visibleSlips : visibleSlips.Take(initialSlippingRequestListLength)
            };
            return View(model);
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
                    return RedirectToAction("Location", new { id=id});
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

        // GET: Slipping/Edit/ID/Location
        [HttpGet]
        public ActionResult Location(int id)
        {
            SlippingRequest slippingRequest = Get(id);
            var model = new LocationAndHours
            {
                ID = slippingRequest.ID,
                Location = slippingRequest.Location != null ? slippingRequest.Location : string.Empty,
                Hours = slippingRequest.TravelTimeInHours.HasValue ? slippingRequest.TravelTimeInHours.ToString() : string.Empty
            };
            return View(model);
        }

        // POST: Slipping/Edit/ID/ToDate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Location(int id, LocationAndHours model)
        {
            SlippingRequest slippingRequest = Get(id);

            if (slippingRequest != null)
            {
                if (ModelState.IsValid)
                {
                    slippingRequest.Location = model.Location;
                    slippingRequest.TravelTimeInHours = Convert.ToInt32(model.Hours);
                    CreateOrUpdate(slippingRequest);
                    return RedirectToAction("Location");
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

        // GET: Slipping/Edit/ID/OppositionMPs
        [HttpGet]
        public ActionResult OppositionMPs(int id)
        {
            SlippingRequest slippingRequest = Get(id);

            if (slippingRequest != null)
            {
                var model = new OppositionMPs
                {
                    YesNo = slippingRequest.OppositionMPsAttending,
                    MPs = slippingRequest.OppositionMPs
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }

        // POST: Slipping/Edit/ID/OppositionMPs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OppositionMPs(int id, OppositionMPs model)
        {
            SlippingRequest slippingRequest = Get(id);

            if (slippingRequest != null)
            {
                if (ModelState.IsValid)
                {
                    //slippingRequest = model.GetDateTime();
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