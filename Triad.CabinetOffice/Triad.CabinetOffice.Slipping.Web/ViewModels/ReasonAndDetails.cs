using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triad.CabinetOffice.Slipping.Data.Models;

namespace Triad.CabinetOffice.Slipping.Web.ViewModels
{
    public class ReasonAndDetails
    {
        public int ID { get; set; }

        public string Details { get; set; }

        [Required]
        public string Reason { get; set; }

        public ICollection<RequestReason> Reasons { get; set; }

        public IDictionary<int, string> Labels = new Dictionary<int, string>()
        {
            {1,"Please provide a description for your reason" },
            {2,"What type of constituency engagement is it?" },
            {3,"Please provide a description for your reason" },
            {5,"What is the personal/other reason?" }
        };

        public IDictionary<int, string> Hints = new Dictionary<int, string>()
        {
            {1,"for example: Select Committee Trip, Delegation on behalf of a group, an All Party Parliamentary related trip" },
            {2,"for example: fundraising, charity event or surgery appointments with constituents" },
            {3,"for example: Select Committee Trip, Delegation on behalf of a group, an All Party Parliamentary related trip" },
            {5,"For example dentist, funeral or wedding. If you do not wish to mention the reason to us, please just write ‘Personal’ and let your Whip know" }
        };

        public IDictionary<int, dynamic> Instructions = new Dictionary<int, dynamic>()
        {
            {1, "What will be the repercussions of the slip being revoked at last minute?"+ Environment.NewLine + "Please state if the trip has been approved by Number 10" },
            {2,"Please also provide the estimated size of the event" },
            {3,"What will be the repercussions of the slip being revoked at last minute?"+ Environment.NewLine + "Can the event be held on the Parliamentary Estate if necessary" },
            {5,""}
          };



    }
}