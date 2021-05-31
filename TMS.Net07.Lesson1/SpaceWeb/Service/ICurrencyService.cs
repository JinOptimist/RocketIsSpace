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
        decimal Convert(decimal amount, Currency currency);

        decimal Convert(Currency currencyFrom,decimal amount, Currency currencyTo);

        bool CheckBalanceToPay(BankAccount account,decimal amount);
    }
}
