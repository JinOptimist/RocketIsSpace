using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.Service
{
    public interface ICurrencyService
    {
        /// <summary>
        /// Amount in $ convert to another currency
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public decimal ConvertAmount( Currency currency);

        public decimal GetExchangeRate(Currency currencyFrom,decimal amount, Currency currencyTo);
        
        decimal Convert(Currency currencyFrom,decimal amount, Currency currencyTo);

        bool CheckBalanceToPay(BankAccount account,decimal amount);
    }
}
