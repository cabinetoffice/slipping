using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping;
using Triad.CabinetOffice.Slipping.Data.Models;

namespace Triad.CabinetOffice.Slipping.Data.Repositories
{
    public class SlippingRepository:RepositoryBase
    {
        public SlippingRepository():base()
        {
        }
        public SlippingRepository(SlippingEntities context):base(context)
        {
        }
        public SlippingRequest Get(int id)
        {
            // Check that there is one, and only one, matching contact
            if (Exists(id))
            {
                AbsenceRequest absenceRequest = db.AbsenceRequests.Find(id);
                SlippingRequest slippingRequest = GetSlippingRequest(absenceRequest);

                return slippingRequest;
            }
            else
            {
                return null;
            }

        }
        public int CreateOrUpdate(SlippingRequest slippingRequest,int userId)
        {
            try
            {
               AbsenceRequest absenceRquest = GetAbsenceRequest(slippingRequest, userId);
                if(absenceRquest.ID==0)
                {
                    db.AbsenceRequests.Add(absenceRquest);
                }
                else
                {
                    db.Entry(absenceRquest).State = EntityState.Modified;
                }

                db.SaveChanges();

                return absenceRquest.ID;

            }
            catch (Exception ex)
            {
                 throw ex;
            }

        }
        private AbsenceRequest GetAbsenceRequest(SlippingRequest slippingRequest,int userId)
        {
            AbsenceRequest absenceRequest;

            if(slippingRequest.ID==0)
            {
                absenceRequest = new AbsenceRequest()
                {
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now
                };
            }
            else
            {
                absenceRequest = db.AbsenceRequests.Find(slippingRequest.ID);
            }

            absenceRequest.LastChangedBy = userId;
            absenceRequest.LastChangedDate = DateTime.Now;
            absenceRequest.MPID = slippingRequest.MPID;
            absenceRequest.ReasonID = slippingRequest.ReasonID;
            absenceRequest.Details = slippingRequest.Details;
            absenceRequest.StatusID = slippingRequest.StatusID;
            absenceRequest.FromDate = slippingRequest.FromDate;
            absenceRequest.ToDate = slippingRequest.ToDate;
            absenceRequest.DecisionNotes = slippingRequest.DecisionNotes;
            absenceRequest.Location = slippingRequest.Location;
            absenceRequest.TravelTimeInHours = slippingRequest.TravelTimeInHours;

             return absenceRequest;
        }
        internal bool Exists(int id)
        {
            AbsenceRequest absenceRequest = db.AbsenceRequests.Find(id);

            if (absenceRequest != null)
            {
                return true;
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
    }
}
