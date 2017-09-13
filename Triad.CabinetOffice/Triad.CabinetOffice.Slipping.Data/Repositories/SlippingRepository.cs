using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.PAWS;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;
using Triad.CabinetOffice.Slipping.Data.Models;

namespace Triad.CabinetOffice.Slipping.Data.Repositories
{
    public class SlippingRepository : RepositoryBase
    {
        public SlippingRepository() : base()
        {
            this.PAWSDB = new PAWSEntities();
        }

        public SlippingRepository(SlippingEntities context, PAWSEntities pawsContext) : base(context)
        {
            this.PAWSDB = pawsContext;
        }

        public SlippingRequest Get(int requestId, int userId)
        {
            if (Exists(requestId, userId))
            {
                AbsenceRequest absenceRequest = db.AbsenceRequests.Find(requestId);
                SlippingRequest slippingRequest = GetSlippingRequest(absenceRequest);
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
            }
            return absenceRequest;
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

        private SlippingRequest GetSlippingRequest(AbsenceRequest absenceRequest)
        {
            SlippingRequest slippingRequest = new SlippingRequest()
            {
                ID= absenceRequest.ID,
                MPID=absenceRequest.MPID,
                ReasonID=absenceRequest.ReasonID,
                Details=absenceRequest.Details,
                StatusID=absenceRequest.StatusID,
                FromDate=absenceRequest.FromDate,
                ToDate=absenceRequest.ToDate,
                DecisionNotes=absenceRequest.DecisionNotes,
                CreatedBy=absenceRequest.CreatedBy,
                LastChangedBy=absenceRequest.LastChangedBy,
                Location=absenceRequest.Location,
                TravelTimeInHours=absenceRequest.TravelTimeInHours
            };

            return slippingRequest;
        }

        public IEnumerable<SlipSummary> GetSummaries(int MPID, int userId)
        {
            List<SlipSummary> result = new List<SlipSummary>();

            if (UserCanActForMP(userId, MPID))
            {
                result.AddRange(this.db.AbsenceRequests
                    .Where(ar => ar.MPID == MPID)
                    .Select(ar => new SlipSummary()
                    {
                        FromDate = ar.FromDate,
                        ToDate = ar.ToDate.HasValue ? ar.ToDate.Value : new DateTime(9999, 1, 1),
                        ID = ar.ID,
                        Status = "Unsubmitted"
                    })
                );

                result.AddRange(this.PAWSDB.Absence_Requests
                    .Where(ar => ar.Govt_MP == MPID)
                    .ToList()
                    .Select(ar => new SlipSummary()
                    {
                        FromDate = ar.From_Date_Time.HasValue ? ar.From_Date_Time.Value : DateTime.Now.Date,
                        ToDate = ar.To_Date_Time.HasValue ? ar.To_Date_Time.Value : new DateTime(9999, 1, 1),
                        ID = ar.ID,
                        Status = ar.Absence_Request_Status.Status
                    })
                );
            }

            return result;
        }
    }
}
