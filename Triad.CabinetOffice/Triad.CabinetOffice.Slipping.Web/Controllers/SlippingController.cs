using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;
using Triad.CabinetOffice.Slipping.Data.Models;
using Triad.CabinetOffice.Slipping.Data.Repositories;
using Triad.CabinetOffice.Slipping.Web.Attributes;
using Triad.CabinetOffice.Slipping.Web.ViewModels;

namespace Triad.CabinetOffice.Slipping.Web.Controllers
{
    [Authorize]
    public class SlippingController : Controller
    {
        #region Properties

        private SlippingRepository SlippingRepository { get { return new SlippingRepository(); } }

        private MPRepository MPRepository { get { return new MPRepository(); } }

        private ReasonRepository ReasonRepository { get { return new ReasonRepository(); } }

        private int UserID
        {
            get
            {
                string username = this.User.Identity.Name;
                UserRepository repository = new UserRepository();
                User user = repository.GetByUsername(username);

                if (user != null)
                {
                    return user.ID;
                }
                else
                {
                    throw new Exception(string.Format("User '{0}' not recognised.", this.User.Identity.Name));
                }
            }
        }

        private int MPID
        {
            get
            {
                string username = this.User.Identity.Name;
                UserRepository repository = new UserRepository();
                User user = repository.GetByUsername(username);

                if (user != null)
                {
                    return user.UserMPs1.First().MPID;
                }
                else
                {
                    throw new Exception(string.Format("User '{0}' not recognised.", this.User.Identity.Name));
                }
            }
        }

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
        private int SubmitSlippingRequest(SlippingRequest slippingRequest)
        {
            return SlippingRepository.SubmitSlippingRequest(slippingRequest, this.UserID);
        }

        private bool IsSubmitted(SlippingRequest slippingRequest)
        {
            return slippingRequest.PawsAbsenceRequestID != null;
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

                if (slippingRequest != null && !IsSubmitted(slippingRequest))
                {
                    model = new DateAndTime
                    {
                        ID = slippingRequest.ID,
                        Date = slippingRequest.FromDate,
                        Hour = slippingRequest.FromDate.ToString("HH"),
                        Minute = slippingRequest.FromDate.ToString("mm")
                    };
                }
                else
                {
                    return RedirectToAction("NotFound");
                }
            }
            return View(model);
        }

        // POST: Slipping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FromDate(int? id, DateAndTime model)
        {
            var fromDate = model.GetDateTime();
            if (fromDate < DateTime.Now.AddMinutes(15))
            {
                ModelState.AddModelError("Hour", "From Time must be at least 15 minutes from now");
            }

            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    // Update an existing record
                    SlippingRequest slippingRequest = Get(id.Value);

                    if (slippingRequest != null && !IsSubmitted(slippingRequest))
                    {
                        slippingRequest.FromDate = model.GetDateTime();
                        CreateOrUpdate(slippingRequest);
                        return RedirectToAction("ToDate", new { id = id });
                    }
                    else
                    {
                        return RedirectToAction("NotFound", "Home");
                    }
                }
                else
                {
                    // Create a new record
                    SlippingRequest slippingRequest = new SlippingRequest();
                    slippingRequest.FromDate = model.GetDateTime();
                    int requestId = CreateOrUpdate(slippingRequest);
                    return RedirectToAction("ToDate", new { id = requestId });
                }
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

            if (slippingRequest != null && !IsSubmitted(slippingRequest))
            {
                var model = new DateAndTime
                {
                    ID = slippingRequest.ID,
                    Date = slippingRequest.ToDate.HasValue ? slippingRequest.ToDate.Value : slippingRequest.FromDate,
                    Hour = slippingRequest.ToDate.HasValue ? slippingRequest.ToDate.Value.ToString("HH") : slippingRequest.FromDate.ToString("HH"),
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

            if (slippingRequest != null && !IsSubmitted(slippingRequest))
            {
                DateTime toDate = model.GetDateTime();

                if (toDate.Date < slippingRequest.FromDate.Date)
                {
                    ModelState.AddModelError("Date", "To Date cannot be before From Date");
                }

                if (toDate.Date == slippingRequest.FromDate.Date)
                {
                    if (toDate.TimeOfDay <= slippingRequest.FromDate.TimeOfDay)
                    {
                        ModelState.AddModelError("Hour", "To Time must be at least 15 minutes after From Time");
                    }
                }

                if (ModelState.IsValid)
                {
                    slippingRequest.ToDate = model.GetDateTime();
                    CreateOrUpdate(slippingRequest);
                    return RedirectToAction("Location", new { id = id });
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
            if (slippingRequest != null && !IsSubmitted(slippingRequest))
            {
                var model = new LocationAndHours
                {
                    ID = slippingRequest.ID,
                    Location = slippingRequest.Location != null ? slippingRequest.Location : string.Empty,
                    Hours = slippingRequest.TravelTimeInHours.HasValue ? slippingRequest.TravelTimeInHours.ToString() : string.Empty
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("NotFound");
            }
        }

        // POST: Slipping/Edit/ID/Location
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Location(int id, LocationAndHours model)
        {
            SlippingRequest slippingRequest = Get(id);

            if (slippingRequest != null && !IsSubmitted(slippingRequest))
            {
                if (ModelState.IsValid)
                {
                    slippingRequest.Location = model.Location;
                    slippingRequest.TravelTimeInHours = Convert.ToInt32(model.Hours);
                    CreateOrUpdate(slippingRequest);
                    return RedirectToAction("Reason");
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

        // GET: Slipping/Edit/ID/Reason
        [HttpGet]
        public ActionResult Reason(int id)
        {
            SlippingRequest slippingRequest = Get(id);
            if (slippingRequest != null && !IsSubmitted(slippingRequest))
            {
                var model = new ReasonAndDetails
                {
                    Reasons = ReasonRepository.Get(),
                    ID = slippingRequest.ID,
                    Reason = slippingRequest.ReasonID.HasValue ? slippingRequest.ReasonID.ToString() : "-1"
                };

                switch (model.Reason)
                {
                    case "1":
                        model.Details1 = slippingRequest.Details ?? string.Empty;
                        break;
                    case "2":
                        model.Details2 = slippingRequest.Details ?? string.Empty;
                        break;
                    case "3":
                        model.Details3 = slippingRequest.Details ?? string.Empty;
                        break;
                    case "5":
                        model.Details5 = slippingRequest.Details ?? string.Empty;
                        break;
                }

                return View(model);
            }
            else
            {
                return RedirectToAction("NotFound");
            }
        }

        // POST: Slipping/Edit/ID/Reason
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reason(int id, ReasonAndDetails model)
        {
            SlippingRequest slippingRequest = Get(id);

            //mapping correct reason details
            switch (model.Reason)
            {
                case "1":
                    if (string.IsNullOrWhiteSpace(model.Details1))
                    {
                        ModelState.AddModelError("Details1", "Detail must be supplied");
                    }
                    break;
                case "2":
                    if (string.IsNullOrWhiteSpace(model.Details2))
                    {
                        ModelState.AddModelError("Details2", "Detail must be supplied");
                    }
                    break;
                case "3":
                    if (string.IsNullOrWhiteSpace(model.Details3))
                    {
                        ModelState.AddModelError("Details3", "Detail must be supplied");
                    }
                    break;
                case "5":
                    if (string.IsNullOrWhiteSpace(model.Details5))
                    {
                        ModelState.AddModelError("Details5", "Detail must be supplied");
                    }
                    break;
            }   

            if (slippingRequest != null && !IsSubmitted(slippingRequest))
            {
                if (ModelState.IsValid)
                {
                    slippingRequest.ReasonID = Convert.ToInt32(model.Reason);
                    switch (model.Reason)
                    {
                        case "1":
                            slippingRequest.Details = model.Details1;
                            break;
                        case "2":
                            slippingRequest.Details = model.Details2;
                            break;
                        case "3":
                            slippingRequest.Details = model.Details3;
                            break;
                        case "5":
                            slippingRequest.Details = model.Details5;
                            break;
                    }

                    CreateOrUpdate(slippingRequest);
                    return RedirectToAction("OppositionMPs");
                }
                else
                {
                    model.Reasons = ReasonRepository.Get();
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

            if (slippingRequest != null && !IsSubmitted(slippingRequest))
            {
                if (slippingRequest.OppositionMPs.Count == 0)
                {
                    slippingRequest.OppositionMPs.Add(new OppositionMP() { ID = 0, MPID = 0, FullName = null });
                }

                var model = new OppositionMPs
                {
                    ID = slippingRequest.ID,
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

            if (slippingRequest != null && !IsSubmitted(slippingRequest))
            {
                if (ModelState.IsValid)
                {
                    slippingRequest.OppositionMPsAttending = model.YesNo;
                    slippingRequest.OppositionMPs = model.MPs;
                    CreateOrUpdate(slippingRequest);
                    return RedirectToAction("CheckYourAnswers");
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

        // GET: Slipping/Edit/ID/CheckYourAnswers
        [HttpGet]
        public ActionResult CheckYourAnswers(int id)
        {
            SlippingRequest slippingRequest = Get(id);

            if (slippingRequest != null && !IsSubmitted(slippingRequest))
            {
                return View(slippingRequest);
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }

        // POST: Slipping/Edit/ID/CheckYourAnswers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckYourAnswers(int id, SlippingRequest model)
        {
            SlippingRequest slippingRequest = Get(id);

            if (slippingRequest != null && !IsSubmitted(slippingRequest))
            {
                if (slippingRequest.FromDate < DateTime.Now.AddMinutes(15))
                {
                    ModelState.AddModelError("FromDate", "From Time must be at least 15 minutes from now");
                }

                if (slippingRequest.ToDate == null)
                {
                    ModelState.AddModelError("ToDate", "To Date and Time must be supplied");
                }
                else
                {
                    if (((DateTime)slippingRequest.ToDate).Date < slippingRequest.FromDate.Date)
                    {
                        ModelState.AddModelError("ToDate", "To Date cannot be before From Date");
                    }

                    if (((DateTime)slippingRequest.ToDate).Date == slippingRequest.FromDate.Date)
                    {
                        if (((DateTime)slippingRequest.ToDate).TimeOfDay <= slippingRequest.FromDate.TimeOfDay)
                        {
                            ModelState.AddModelError("ToDate", "To Time must be at least 15 minutes after From Time");
                        }
                    }
                }

                if (string.IsNullOrEmpty(slippingRequest.Location))
                {
                    ModelState.AddModelError("Location", "Location must be supplied");
                }

                if (slippingRequest.TravelTimeInHours == null)
                {
                    ModelState.AddModelError("TravelTimeInHours", "Travel time to Westminster must be supplied");
                }

                if (slippingRequest.ReasonID == null)
                {
                    ModelState.AddModelError("Reason", "Reason must be supplied");
                }

                if (string.IsNullOrEmpty(slippingRequest.Details))
                {
                    ModelState.AddModelError("Details", "Details must be supplied");
                }

                if (slippingRequest.OppositionMPsAttending == null)
                {
                    ModelState.AddModelError("OppositionMPsAttending", "Any opposition MPs in attendance? must be supplied");
                }

                if (ModelState.IsValid)
                {

                    SubmitSlippingRequest(slippingRequest);
                    return RedirectToAction("Confirmation");
                }
                else
                {
                    return View(slippingRequest);
                }
            }
            else
            {
                return RedirectToAction("NotFound");
            }
        }

        // GET: Slipping/Edit/ID/Confirmation
        [HttpGet]
        public ActionResult Confirmation(int id)
        {
            SlippingRequest slippingRequest = Get(id);

            if (slippingRequest != null)
            {
                return View(slippingRequest);
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }

        // GET: Slipping/Review/ID
        [HttpGet]
        public ActionResult Review(int id)
        {
            SlippingRequest slippingRequest = Get(id);

            if (slippingRequest != null)
            {
                return View(slippingRequest);
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }

        #endregion Action Methods
    }
}