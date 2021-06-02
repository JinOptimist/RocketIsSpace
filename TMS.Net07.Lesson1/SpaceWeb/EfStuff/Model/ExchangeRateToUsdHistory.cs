using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class ExchangeRateToUsdHistory : BaseModel
    {
        public Currency Currency { get; set; }
        public TypeOfExchange TypeOfExch { get; set; }
        public decimal ExchRate { get; set; }
        public DateTime ExchRateDate { get; set; }
    }
}
