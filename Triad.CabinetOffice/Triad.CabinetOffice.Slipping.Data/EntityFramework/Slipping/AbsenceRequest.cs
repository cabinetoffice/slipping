//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Triad.CabinetOffice.Slipping.Data.EntityFramework.Slipping
{
    using System;
    using System.Collections.Generic;
    
    public partial class AbsenceRequest
    {
        public int ID { get; set; }
        public Nullable<int> MPID { get; set; }
        public Nullable<int> ReasonID { get; set; }
        public string Details { get; set; }
        public int StatusID { get; set; }
        public System.DateTime FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string DecisionNotes { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastChangedBy { get; set; }
        public System.DateTime LastChangedDate { get; set; }
        public string Location { get; set; }
        public Nullable<int> TravelTimeInHours { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
