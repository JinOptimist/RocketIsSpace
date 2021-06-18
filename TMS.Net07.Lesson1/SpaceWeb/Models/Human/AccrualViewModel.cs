using System;
using System.Collections.Generic;

namespace SpaceWeb.Models.Human
{
    public class AccrualViewModel
    {
        public long IdEmploye { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public DateTime InviteDate { get; set; }
        public List<DateTime> NoAccrualsDates { get; set; }
    }
}