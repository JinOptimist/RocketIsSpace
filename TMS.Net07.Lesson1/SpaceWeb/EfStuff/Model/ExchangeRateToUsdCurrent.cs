using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class ExchangeRateToUsdCurrent : BaseModel
    {
        public Currency Currency { get; set; }
        public string TypeOfExch { get; set; }
        public decimal ExchRate { get; set; }
    }
}
