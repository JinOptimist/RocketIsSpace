using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.Service
{
    public class CurrencyService : ICurrencyService
    {
        public decimal Convert(decimal amount, Currency currency)
        {
            //TODO USer DB
            return 0;
        }

        public decimal Convert(Currency currencyFrom, decimal amount, Currency currencyTo)
        {
            //TODO USer DB
            return 0;
        }

        public bool CheckBalanceToPay(BankAccount account, decimal amount)
        {
            return false;
        }
    }
}
