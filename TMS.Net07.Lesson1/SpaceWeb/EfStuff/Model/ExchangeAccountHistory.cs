using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class ExchangeAccountHistory : BaseModel
    {
        public Currency CurrencyFrom { get; set; }
        public Currency CurrencyTo { get; set; }
        public string TypeOfExch { get; set; }
        public decimal ExchRate { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExchDate { get; set; }
        public virtual User Owner { get; set; }
    }
}
