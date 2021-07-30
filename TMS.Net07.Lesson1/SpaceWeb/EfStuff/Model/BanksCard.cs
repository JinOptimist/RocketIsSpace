using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class BanksCard : BaseModel
    {
        public Currency Currency { get; set; }
        public EnumBankCard Card { get; set; }
        public string PinCard { get; set; }
        public virtual User Owner { get; set; }
        public int Count { get; set; }

        public string CardUrl { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual List<TransactionBank> TransactionsFrom { get; set; }
        public virtual List<TransactionBank> TransactionsTo { get; set; }
    }
}
