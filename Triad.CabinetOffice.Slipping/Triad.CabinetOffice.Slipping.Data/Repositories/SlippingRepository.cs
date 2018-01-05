using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Configuration;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;
using Triad.CabinetOffice.Slipping.Data.Models;

namespace Triad.CabinetOffice.Slipping.Data.Repositories
{
    public class SlippingRepository : RepositoryBase
    {
        private const string SlipDetailsFormat = "Location: {0} \nDetails: {1} {2}";
        private const string SlipDetailsOppositionMPsFormat = "\nOpposition MPs attending: {0}";
        private int DefaultSlipStatusId = Convert.ToInt32(WebConfigurationManager.AppSettings["DefaultAbsenceRequestStatusID"]);

        public SlippingRepository() : base()
        {
            db = new SlippingEntities();
        }

        public SlippingRepository(SlippingEntities context) : base(context)
        {
            db = context;
        }

        public SlippingRequest Get(int requestId, int userId)
        {
            if (Exists(requestId, userId))
            {
                AbsenceRequest absenceRequest = db.AbsenceRequests.Find(requestId);
                var absenceRequestOppositionMPs = absenceRequest.AbsenceRequestOppositionMPs;
                SlippingRequest slippingRequest = GetSlippingRequest(absenceRequest, absenceRequestOppositionMPs);
                return slippingRequest;
            }
            else
            {
                return null;
            }
        }

        public int CreateOrUpdate(SlippingRequest slippingRequest, int MPID, int userId)
        {
            AbsenceRequest absenceRequest = GetAbsenceRequest(slippingRequest, MPID, userId);
            if (absenceRequest.ID == 0)
            {
                db.AbsenceRequests.Add(absenceRequest);
            }
            else
            {
                db.Entry(absenceRequest).State = EntityState.Modified;
            }

            if (absenceRequest.OppositionMPsAttending == true)
            {
                var removedMPs = new List<AbsenceRequestOppositionMP>();
                foreach(var mp in absenceRequest.AbsenceRequestOppositionMPs)
                {
                    if (!slippingRequest.OppositionMPs.Select(m => m.ID).Contains(mp.ID)) {
                        removedMPs.Add(mp);
                    }
                }
                db.AbsenceRequestOppositionMPs.RemoveRange(removedMPs);

                foreach(var mp in slippingRequest.OppositionMPs)
                {
                    var absenceRequestOppositionMP = GetAbsenceRequestOppositionMP(mp, absenceRequest.ID, userId);
                    if (absenceRequestOppositionMP.ID == 0)
                    {
                        db.AbsenceRequestOppositionMPs.Add(absenceRequestOppositionMP);
                    }
                    else
                    {
                        db.Entry(absenceRequestOppositionMP).State = EntityState.Modified;
                    }
                }
            }
            else
            {
                foreach (var mp in db.AbsenceRequestOppositionMPs.Where(m => m.AbsenceRequestID == absenceRequest.ID))
                    db.AbsenceRequestOppositionMPs.Remove(mp);
            }

            db.SaveChanges();

            return absenceRequest.ID;
        }

        private AbsenceRequest GetAbsenceRequest(SlippingRequest slippingRequest, int MPID, int userId)
        {
            AbsenceRequest absenceRequest;

            if (slippingRequest.ID == 0)
            {
                absenceRequest = new AbsenceRequest()
                {
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    MPID = MPID
                };
            }
            else
            {
                absenceRequest = db.AbsenceRequests.Find(slippingRequest.ID);
            }

            if (absenceRequest != null)
            {
                absenceRequest.LastChangedBy = userId;
                absenceRequest.LastChangedDate = DateTime.Now;
                absenceRequest.ReasonID = slippingRequest.ReasonID;
                absenceRequest.Details = slippingRequest.Details;
                absenceRequest.StatusID = slippingRequest.StatusID;
                absenceRequest.FromDate = slippingRequest.FromDate;
                absenceRequest.ToDate = slippingRequest.ToDate;
                absenceRequest.DecisionNotes = slippingRequest.DecisionNotes;
                absenceRequest.Location = slippingRequest.Location;
                absenceRequest.TravelTimeInHours = slippingRequest.TravelTimeInHours;
                absenceRequest.OppositionMPsAttending = slippingRequest.OppositionMPsAttending;
            }

            return absenceRequest;
        }

        private AbsenceRequestOppositionMP GetAbsenceRequestOppositionMP(OppositionMP oppositionMP, int absenceRequestId, int userId) {
            AbsenceRequestOppositionMP absenceRequestOppositionMP;
            if (oppositionMP.ID == 0)
            {
                absenceRequestOppositionMP = new AbsenceRequestOppositionMP()
                {
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now,
                    MPFullName = oppositionMP.FullName,
                    MPID = oppositionMP.MPID
                };
            }
            else
            {
                absenceRequestOppositionMP = db.AbsenceRequestOppositionMPs.Find(oppositionMP.ID);
            }

            if (absenceRequestOppositionMP != null)
            {
                absenceRequestOppositionMP.AbsenceRequestID = absenceRequestId;
                absenceRequestOppositionMP.LastChangedBy = userId;
                absenceRequestOppositionMP.LastChangedDate = DateTime.Now;
                absenceRequestOppositionMP.MPFullName = oppositionMP.FullName;
                absenceRequestOppositionMP.MPID = oppositionMP.MPID;
            }
            return absenceRequestOppositionMP;
        }

        internal bool Exists(int requestId, int userId)
        {
            AbsenceRequest absenceRequest = db.AbsenceRequests.Find(requestId);

            if (absenceRequest != null)
            {
                return UserCanActForMP(userId, absenceRequest.MPID);
            }
            else
            {
                return false;
            }
        }

        private SlippingRequest GetSlippingRequest(AbsenceRequest absenceRequest, ICollection<AbsenceRequestOppositionMP> absenceRequestOppositionMPs)
        {
            SlippingRequest slippingRequest = new SlippingRequest()
            {
                ID = absenceRequest.ID,
                MPID = absenceRequest.MPID,
                ReasonID = absenceRequest.ReasonID,
                Reason = absenceRequest.ReasonID.HasValue ? absenceRequest.AbsenceRequestReason.Reason : string.Empty,
                Details = absenceRequest.Details,
                StatusID = absenceRequest.StatusID,
                Status = absenceRequest.AbsenceRequestStatus.Status,
                FromDate = absenceRequest.FromDate,
                ToDate = absenceRequest.ToDate,
                DecisionNotes = absenceRequest.DecisionNotes,
                CreatedBy = (int)absenceRequest.CreatedBy,
                LastChangedBy = absenceRequest.LastChangedBy,
                Location = absenceRequest.Location,
                TravelTimeInHours = absenceRequest.TravelTimeInHours,
                OppositionMPsAttending = absenceRequest.OppositionMPsAttending,
                OppositionMPs = absenceRequestOppositionMPs.Count > 0 ? absenceRequestOppositionMPs.Select(a => new OppositionMP { ID = a.ID, MPID = a.MPID, FullName = a.MPFullName }).ToList() : new List<OppositionMP>()
            };

            return slippingRequest;
        }

        public IEnumerable<SlipSummary> GetSummaries(int MPID, int userId)
        {
            IEnumerable<SlipSummary> result = new List<SlipSummary>();

            if (UserCanActForMP(userId, MPID))
            {
                result = db.AbsenceRequests.Where(a => a.MPID == MPID && a.StatusID != 0).ToList().Select(ar => new SlipSummary
                {
                    FromDate = ar.FromDate,
                    ToDate = (DateTime)ar.ToDate,
                    ID = ar.ID,
                    Status = ar.AbsenceRequestStatus.Status,
                    IsUnsubmitted = false,
                    MPID = ar.MPID,
                    Details = ar.Details,
                    Location = ar.Location,
                    TravelTimeInHours = (int)ar.TravelTimeInHours,
                    Reason = ar.AbsenceRequestReason.Reason,
                    OppositionMPsAttending = (bool)ar.OppositionMPsAttending,
                    OppositionMPs = ar.AbsenceRequestOppositionMPs.Count > 0 ? ar.AbsenceRequestOppositionMPs.Select(a => new OppositionMP { ID = a.ID, MPID = a.MPID, FullName = a.MPFullName }).ToList() : new List<OppositionMP>(),
                });
            }

            return result;
        }

        public int SubmitSlippingRequest(SlippingRequest slippingRequest, int userId)
        {
            if (UserCanActForMP(userId, slippingRequest.MPID))
            {
                var absenceRequest = db.AbsenceRequests.Find(slippingRequest.ID).StatusID = 1;
                db.SaveChanges();
                return slippingRequest.ID;
            }
            throw new Exception("Unauthorised");
        }

        public bool CancelSlip(int userId, SlipSummary slip)
        {
            if (UserCanActForMP(userId, slip.MPID))
            {
                var ar = db.AbsenceRequests.Find(slip.ID);
                ar.StatusID = 7; // Cancelled
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("Unauthorised");
            }
            
        }

        public bool DatesOverlapExistingSlip(int MPID, DateTime fromDate)
        {
            return db.AbsenceRequests.Where(ar => ar.MPID == MPID && ar.StatusID != 7 && fromDate > ar.FromDate && fromDate < ar.ToDate).Count() > 0;
        }

        public bool DatesOverlapExistingSlip(int MPID, DateTime fromDate, DateTime toDate)
        {
            return db.AbsenceRequests.Where(ar => ar.MPID == MPID && ar.StatusID != 7 && fromDate < ar.ToDate && ar.FromDate < toDate).Count() > 0;
        }
    }
}
