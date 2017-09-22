using Notify.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        private User SlippingUser
        {
            get
            {
                var username = User.Identity.Name;
                var repository = new UserRepository();
                var user = repository.GetByUsername(username);

                if (user != null)
                {
                    return user;
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

        private string GovUkNotifyApiKey = WebConfigurationManager.AppSettings["GovUkNotifyApiKey"];

        private string NotifyTemplateId_SlippingRequestReceivedUser = WebConfigurationManager.AppSettings["NotifyTemplateId_SlippingRequestReceivedUser"];

        private string NotifyTemplateId_SlippingRequestReceivedAdmin = WebConfigurationManager.AppSettings["NotifyTemplateId_SlippingRequestReceivedAdmin"];

        private string SlippingRequestReviewersEmailAddress = WebConfigurationManager.AppSettings["SlippingRequestReviewersEmailAddress"];

        private string NotifyTemplateId_SlippingRequestCancelledUser = WebConfigurationManager.AppSettings["NotifyTemplateId_SlippingRequestCancelledUser"];

        private string NotifyTemplateId_SlippingRequestCancelledAdmin = WebConfigurationManager.AppSettings["NotifyTemplateId_SlippingRequestCancelledAdmin"];

        #endregion Properties

        #region Methods

        private SlippingRequest Get(int requestId)
        {
            return SlippingRepository.Get(requestId, SlippingUser.ID);
        }

        private int CreateOrUpdate(SlippingRequest slippingRequest)
        {
            return SlippingRepository.CreateOrUpdate(slippingRequest, this.MPID, SlippingUser.ID);
        }
        private int SubmitSlippingRequest(SlippingRequest slippingRequest)
        {
            return SlippingRepository.SubmitSlippingRequest(slippingRequest, SlippingUser.ID);
        }

        private bool IsSubmitted(SlippingRequest slippingRequest)
        {
            return slippingRequest.PawsAbsenceRequestID != null;
        }

        private void SendNotification(string templateId, string emailAddress, Dictionary<string,dynamic> personalisations)
        {
            if (!string.IsNullOrEmpty(GovUkNotifyApiKey))
            {
                var client = new NotificationClient(GovUkNotifyApiKey);
                var response = client.SendEmail(emailAddress, templateId, personalisations);
            }
            else
            {
                throw new Exception("GOV.UK Notify API Key in web.config missing or invalid.");
            }

        }
        
        private string GetUserEmailAddress(User user)
        {
            return user.Username.Replace("live.com#", string.Empty);
        }

        private string GetMPEmailAddress(int MPID, int userId)
        {
            var mp = MPRepository.Get(MPID, userId);
            if (mp.EmailAddress != null)
            {
                return mp.EmailAddress;
            }
            else
            {
                throw new Exception(string.Format("Email address for {0} MP missing in PAWS", mp.Name));
            }
        }

        private bool CancelSlip(SlipSummary slip, int userId)
        {
            return SlippingRepository.CancelSlip(userId, slip);
        }

        #endregion Methods

        #region Action Methods

        // GET: Slipping
        public ActionResult Index(bool viewAll = false)
        {
            MP mp = MPRepository.Get(this.MPID, SlippingUser.ID);
            int initialSlippingRequestListLength = Convert.ToInt32(WebConfigurationManager.AppSettings["InitialSlippingRequestListLength"]);
            IEnumerable<SlipSummary> slips = this.SlippingRepository.GetSummaries(this.MPID, SlippingUser.ID);

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
                    SlippingRequest slippingRequest = new SlippingRequest()
                    {
                        FromDate = model.GetDateTime()
                    };
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
                    Date = slippingRequest.ToDate ?? slippingRequest.FromDate,
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
                    Location = slippingRequest.Location ?? string.Empty,
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
                    case "4":
                        model.Details4 = slippingRequest.Details ?? string.Empty;
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
                        ModelState.AddModelError("Details1", "A description of the reason is required");
                    }
                    break;
                case "2":
                    if (string.IsNullOrWhiteSpace(model.Details2))
                    {
                        ModelState.AddModelError("Details2", "A description of the reason is required");
                    }
                    break;
                case "3":
                    if (string.IsNullOrWhiteSpace(model.Details3))
                    {
                        ModelState.AddModelError("Details3", "A description of the reason is required");
                    }
                    break;
                case "4":
                    if (string.IsNullOrWhiteSpace(model.Details4))
                    {
                        ModelState.AddModelError("Details4", "A description of the reason is required");
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
                        case "4":
                            slippingRequest.Details = model.Details4;
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
                if(slippingRequest.OppositionMPs ==null)
                {
                    slippingRequest.OppositionMPs = new List<OppositionMP>();
                }

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
                    if (model.YesNo == null && model.MPs == null)
                    {
                        model.MPs = new List<OppositionMP>() { new OppositionMP() };
                    }
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

                    if (!string.IsNullOrEmpty(NotifyTemplateId_SlippingRequestReceivedUser))
                    {
                        if (!SlippingUser.IsMP)
                        {
                            SendNotification(NotifyTemplateId_SlippingRequestReceivedUser, GetUserEmailAddress(SlippingUser), new Dictionary<string, dynamic>()
                            {
                                { "name", SlippingUser.Forenames },
                                { "absence_date", slippingRequest.FromDate.ToString("dd/MM/yyyy") },
                                { "reference", slippingRequest.PawsAbsenceRequestID }
                            });
                        }
                        SendNotification(NotifyTemplateId_SlippingRequestReceivedUser, GetMPEmailAddress(slippingRequest.MPID, SlippingUser.ID), new Dictionary<string, dynamic>()
                        {
                            { "name", SlippingUser.Forenames },
                            { "absence_date", slippingRequest.FromDate.ToString("dd/MM/yyyy") },
                            { "reference", slippingRequest.PawsAbsenceRequestID }
                        });
                    }
                    else
                    {
                        throw new Exception("NotifyTemplateId_SlippingRequestReceivedUser in web.config missing or invalid");
                    }
                    if (!string.IsNullOrEmpty(NotifyTemplateId_SlippingRequestReceivedAdmin) && !string.IsNullOrEmpty(SlippingRequestReviewersEmailAddress))
                    {
                        SendNotification(NotifyTemplateId_SlippingRequestReceivedAdmin, SlippingRequestReviewersEmailAddress, new Dictionary<string, dynamic>()
                        {
                            { "name", string.Format("{0} {1}", SlippingUser.Forenames, SlippingUser.Surname) },
                            { "absence_date", slippingRequest.FromDate.ToString("dd/MM/yyyy") },
                            { "reference", slippingRequest.PawsAbsenceRequestID }
                        });
                    }
                    else
                    {
                        throw new Exception("NotifyTemplateId_SlippingRequestReceivedAdmin and/or SlippingRequestReviewersEmailAddress in web.config missing or invalid");
                    }
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
            var slip = SlippingRepository.GetSummaries(MPID, SlippingUser.ID).FirstOrDefault(s => s.ID == id);

            if (slip != null)
            {
                return View(slip);
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }

        // POST: Slipping/Review/ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review(int id, SlipSummary model)
        {
            var slip = SlippingRepository.GetSummaries(MPID, SlippingUser.ID).FirstOrDefault(s => s.ID == id);
            if (slip != null && slip.Status != "Cancelled")
            {
                if (CancelSlip(slip, SlippingUser.ID))
                {
                    if (!string.IsNullOrEmpty(NotifyTemplateId_SlippingRequestCancelledUser))
                    {
                        if (!SlippingUser.IsMP)
                        {
                            SendNotification(NotifyTemplateId_SlippingRequestCancelledUser, GetUserEmailAddress(SlippingUser), new Dictionary<string, dynamic>()
                            { });
                        }
                        SendNotification(NotifyTemplateId_SlippingRequestCancelledUser, GetMPEmailAddress(slip.MPID, SlippingUser.ID), new Dictionary<string, dynamic>()
                        { });
                    }
                    else
                    {
                        throw new Exception("NotifyTemplateId_SlippingRequestCancelledUser in web.config missing or invalid");
                    }
                    if (!string.IsNullOrEmpty(NotifyTemplateId_SlippingRequestCancelledAdmin) && !string.IsNullOrEmpty(SlippingRequestReviewersEmailAddress))
                    {
                        SendNotification(NotifyTemplateId_SlippingRequestCancelledAdmin, SlippingRequestReviewersEmailAddress, new Dictionary<string, dynamic>()
                        { });
                    }
                    else
                    {
                        throw new Exception("NotifyTemplateId_SlippingRequestCancelledAdmin and/or SlippingRequestReviewersEmailAddress in web.config missing or invalid");
                    }
                    return RedirectToAction("Cancelled");
                }
                else
                {
                    ModelState.AddModelError("ID", "Error cancelling slip. Please try again later or contact the Whips Office.");
                    return View(slip);
                }
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }

        // GET: Slipping/Deleted/Date
        [HttpGet]
        public ActionResult Deleted(string date)
        {
            DateTime validatedDate;
            if (DateTime.TryParse(date.Replace("!", ":"), out validatedDate))
            {
                return View(new DeletedSlippingRequest(validatedDate));
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }

        // GET: Slipping/Review/ID/Cancelled
        [HttpGet]
        public ActionResult Cancelled(int id)
        {
            var slip = SlippingRepository.GetSummaries(MPID, SlippingUser.ID).FirstOrDefault(s => s.ID == id);

            if (slip != null)
            {
                return View(slip);
            }
            else
            {
                return RedirectToAction("NotFound", "Home");
            }
        }

        #endregion Action Methods
    }
}