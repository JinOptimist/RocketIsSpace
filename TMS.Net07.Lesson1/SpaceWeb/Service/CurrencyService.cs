using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Service
{
    public class CurrencyService : ICurrencyService
    {
        private UserService _userService;
        private ExchangeRateToUsdCurrentRepository _exchangeRateToUsdCurrentRepository;
        private ExchangeAccountHistoryRepository _exchangeAccountHistoryRepository;

        public CurrencyService(UserService userService,
            ExchangeRateToUsdCurrentRepository exchangeRateToUsdCurrentRepository,
            ExchangeAccountHistoryRepository exchangeAccountHistoryRepository)
        {
            _userService = userService;
            _exchangeRateToUsdCurrentRepository = exchangeRateToUsdCurrentRepository;
            _exchangeAccountHistoryRepository = exchangeAccountHistoryRepository;
        }

        public decimal ConvertByAlex(decimal amount, Currency currency)
        {
            var user = _userService.GetCurrent();
            var typeOfExchange = TypeOfExchange.Sell;
            var exchangeRate = _exchangeRateToUsdCurrentRepository.GetExchangeRate(currency, typeOfExchange);

            var convertedAmount = amount * exchangeRate.ExchRate;
            
            return convertedAmount;
        }

        public decimal ConvertByAlex(Currency currencyFrom, decimal amount, Currency currencyTo)
        {
            var user = _userService.GetCurrent();
            var typeOfExchangeToUsd = TypeOfExchange.Buy;
            var typeOfExchangeUsdToCurrencyFrom = TypeOfExchange.Sell;
            var exchRateToUsd = _exchangeRateToUsdCurrentRepository.GetExchangeRate(currencyFrom, typeOfExchangeToUsd).ExchRate;
            var exchRateUsdToCurrencyFrom = _exchangeRateToUsdCurrentRepository.GetExchangeRate(currencyTo, typeOfExchangeUsdToCurrencyFrom).ExchRate;

            var convertedAmount = amount / exchRateToUsd * exchRateUsdToCurrencyFrom;

            return convertedAmount;
        }

        public void PutToExchangeAccountHistory(Currency currencyFrom, Currency currencyTo, TypeOfExchange typeOfExch,
            decimal exchRate, decimal amount, User owner)
        {
            var currentDate = DateTime.Now;

            var exchangeAccountHistoryDb = new ExchangeAccountHistory
            {
                CurrencyFrom = currencyFrom,
                CurrencyTo = currencyTo,
                TypeOfExch = typeOfExch,
                ExchRate = exchRate,
                Amount = amount,
                ExchDate = currentDate,
                Owner = owner

            };
            _exchangeAccountHistoryRepository.Save(exchangeAccountHistoryDb);
        }
    }
}
