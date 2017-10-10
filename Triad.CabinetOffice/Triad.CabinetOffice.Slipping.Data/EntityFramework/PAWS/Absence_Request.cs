//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Triad.CabinetOffice.Slipping.Data.EntityFramework.PAWS
{
    using System;
    using System.Collections.Generic;
    
    public partial class Absence_Request
    {
        public int ID { get; set; }
        public int Govt_MP { get; set; }
        public int Reason { get; set; }
        public string Details { get; set; }
        public System.DateTime Date_Created { get; set; }
        public int Status { get; set; }
        public System.TimeSpan From_Time { get; set; }
        public System.TimeSpan To_Time { get; set; }
        public System.DateTime From_Date { get; set; }
        public System.DateTime To_Date { get; set; }
        public Nullable<System.DateTime> From_Date_Time { get; set; }
        public Nullable<System.DateTime> To_Date_Time { get; set; }
        public string Decision_Notes { get; set; }
    
        public virtual Absence_Request_Status Absence_Request_Status { get; set; }
        public virtual Absence_Request_Reason Absence_Request_Reason { get; set; }
        public virtual Members_of_Parliament Member_of_Parliament { get; set; }
    }
}
