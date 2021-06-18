using System;

namespace SpaceWeb.EfStuff.Model
{
    public class Accrual : BaseModel
    {
        public virtual Employe Employe { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}