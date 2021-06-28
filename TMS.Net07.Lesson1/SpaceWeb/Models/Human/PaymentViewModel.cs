using System;

namespace SpaceWeb.Models.Human
{
    public class PaymentViewModel
    {
        public long IdEmploye { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Payed { get; set; }
        public decimal NotPayed { get; set; }
        public string AccountNumber { get; set; }
        public string DepartmentAccountNumber { get; set; }
    }
}
