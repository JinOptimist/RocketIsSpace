using System;

namespace SpaceWeb.EfStuff.Model
{
    public class Payment : BaseModel
    {
        public virtual Employe Employe { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        //public virtual BankAccount PayedFrom { get; set; }
    }
}