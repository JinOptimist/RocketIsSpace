using SpaceWeb.Models.CustomValidationAttribute;
using System;
using System.Collections.Generic;

namespace SpaceWeb.Models.Human
{
    public class PaymentViewModel
    {
        public long EmployeId { get; set; }
        public DateTime Date { get; set; }
        [Min(0)]
        public decimal Amount { get; set; }
        public decimal Payed { get; set; }
        public decimal NotPayed { get; set; }
    }
}
