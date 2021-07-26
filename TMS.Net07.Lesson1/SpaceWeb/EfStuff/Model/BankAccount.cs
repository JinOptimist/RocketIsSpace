using SpaceWeb.EfStuff.Model.Enum;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class BankAccount : BaseModel
    {
        public string AccountNumber { get; set; }
        public Currency Currency { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public virtual User Owner { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public virtual List<BanksCard> BanksCards { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public BankAccountType BankAccountType { get; set; }
        public virtual List<Transaction> IncomingTransactions { get; set; }
        public virtual List<Transaction> OutcomingTransactions { get; set; }
        public bool IsFrozen { get; set; }
    }
}
