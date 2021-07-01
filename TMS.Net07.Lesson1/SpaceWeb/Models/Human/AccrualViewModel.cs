﻿using System;
using System.Collections.Generic;

namespace SpaceWeb.Models.Human
{
    public class AccrualViewModel
    {
        public long EmployeId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<DateTime> NoAccrualsDates { get; set; }
    }
}