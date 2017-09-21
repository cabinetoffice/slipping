using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.PAWS;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;
using Triad.CabinetOffice.Slipping.Data.Models;
using Triad.CabinetOffice.Slipping.Data.Extensions;

namespace Triad.CabinetOffice.Slipping.Data.Repositories
{
    public class SlippingRepository : RepositoryBase
    {
        private const string SlipDetailsFormat = "Location: {0} \nTravel Time to Westminster (hours): {1} \nReason: {2} \nDetails: {3} {4}";
        private int DefaultSlipStatusId = Convert.ToInt32(WebConfigurationManager.AppSettings["DefaultAbsenceRequestStatusID"]);
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
                absenceRequest.PawsAbsenceRequestID = slippingRequest.PawsAbsenceRequestID;
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
                Reason = absenceRequest.ReasonID.HasValue ? new ReasonRepository().Get(absenceRequest.ReasonID.Value).Reason : string.Empty,
                Details = absenceRequest.Details,
                StatusID = absenceRequest.StatusID,
                Status= absenceRequest.PawsAbsenceRequestID.HasValue ?  this.PAWSDB.Absence_Requests.Where(ar=>ar.ID== absenceRequest.PawsAbsenceRequestID).FirstOrDefault().Absence_Request_Status.Status : "Unsubmitted",
                FromDate = absenceRequest.FromDate,
                ToDate = absenceRequest.ToDate,
                DecisionNotes = absenceRequest.DecisionNotes,
                CreatedBy = absenceRequest.CreatedBy,
                LastChangedBy = absenceRequest.LastChangedBy,
                Location = absenceRequest.Location,
                TravelTimeInHours = absenceRequest.TravelTimeInHours,
                OppositionMPsAttending = absenceRequest.OppositionMPsAttending,
                OppositionMPs = absenceRequestOppositionMPs.Select(a => new OppositionMP { ID = a.ID, MPID = a.MPID, FullName = a.MPFullName }).ToList(),
                PawsAbsenceRequestID = absenceRequest.PawsAbsenceRequestID
            };

            return slippingRequest;
        }

        public IEnumerable<SlipSummary> GetSummaries(int MPID, int userId)
        {
            IEnumerable<SlipSummary> result = new List<SlipSummary>();



            if (UserCanActForMP(userId, MPID))
            {

                IList<Absence_Request> absence_Request = this.PAWSDB.Absence_Requests.ToList();
                IList<AbsenceRequest> absenceRequest = db.AbsenceRequests.Where(a => a.MPID == MPID).ToList();


                result = absenceRequest.GroupJoin(
                         absence_Request,
                         foo => foo.PawsAbsenceRequestID,
                         bar => bar.ID,
                         (f, bs) => new { Foo = f, Bar = bs.FirstOrDefault() })
                         .Select(r => new SlipSummary {
                             FromDate = r.Foo.FromDate,
                             ToDate = r.Foo.ToDate ?? new DateTime(9999, 1, 1),
                             ID = r.Foo.ID,
                             Status = r.Foo.PawsAbsenceRequestID.HasValue ? r.Bar.Absence_Request_Status.Status : "Unsubmitted",
                             IsUnsubmitted = r.Foo.PawsAbsenceRequestID.HasValue ? false : true
                        });

             }

            return result;
        }

        public int SubmitSlippingRequest(SlippingRequest slippingRequest, int userId)
        {
            if (UserCanActForMP(userId, slippingRequest.MPID))
            {
                var absenceRequest = PAWSDB.Absence_Requests.Add(
                    new Absence_Request()
                    {
                        Govt_MP = slippingRequest.MPID,
                        Reason = (int)slippingRequest.ReasonID,
                        Details = string.Format(SlipDetailsFormat,
                                        slippingRequest.Location,
                                        slippingRequest.TravelTimeInHours,
                                        slippingRequest.Reason,
                                        slippingRequest.Details,
                                        ((bool)slippingRequest.OppositionMPsAttending) ?
                                            string.Format("\nOpposition MPs attending: {0}", string.Join(", ", slippingRequest.OppositionMPs.Select(mp => mp.FullName))) :
                                            string.Empty
                                        ).Left(220),
                        Date_Created = DateTime.Now,
                        Status = DefaultSlipStatusId,
                        From_Time = slippingRequest.FromDate.TimeOfDay,
                        From_Date = slippingRequest.FromDate.Date,
                        To_Time = ((DateTime)slippingRequest.ToDate).TimeOfDay,
                        To_Date = ((DateTime)slippingRequest.ToDate).Date
                    });
                PAWSDB.SaveChanges();
                slippingRequest.PawsAbsenceRequestID = absenceRequest.ID;
                CreateOrUpdate(slippingRequest, slippingRequest.MPID, userId);
                return slippingRequest.ID;
            }
            throw new Exception("Unauthorised");
        }

        public bool CancelSlip(int userId, SlippingRequest slip)
        {
            if (UserCanActForMP(userId, slip.MPID))
            {
                var ar = PAWSDB.Absence_Requests.Find(slip.PawsAbsenceRequestID);
                ar.Status = 7; // Cancelled
                try
                {
                    PAWSDB.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("Unauthorised");
            }
            
        }
    }
}
