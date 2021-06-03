using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceWeb.EfStuff.Repositories;
using AutoMapper;

namespace SpaceWeb.Service
{
    public interface ICurrencyService
    {
        bool CheckBalanceToPay(BankAccount account,decimal amount);
        decimal ConvertByAlex(decimal amount, Currency currency);
        decimal ConvertByAlex(Currency currencyFrom,decimal amount, Currency currencyTo);
        void PutToExchangeAccountHistory(Currency currencyFrom, Currency currencyTo, TypeOfExchange typeOfExch,
            decimal exchRate, decimal amount, User owner);
        public GottenCurrency GetExchangeRates();
        public void PutCurrentExchangeRatesToDb(ExchangeRateToUsdCurrentRepository _exchangeRateToUsdCurrentRepository, GottenCurrency exchangeRates);
        public void DeleteCurrentExchRatesFromDb(ExchangeRateToUsdCurrentRepository _exchangeRateToUsdCurrentRepository);
        public void MoveCurrentExchangesDbToHistoryDb(ExchangeRateToUsdCurrentRepository _exchangeRateToUsdCurrentRepository,
            ExchangeRateToUsdHistoryRepository _exchangeRateToUsdHistoryRepository, Mapper _mapper);
    }
}
