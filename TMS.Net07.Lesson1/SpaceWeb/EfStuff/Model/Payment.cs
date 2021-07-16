using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceWeb.EfStuff.Model
{
    public class Payment : BaseModel
    {
        public virtual Employe Employe { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }
}