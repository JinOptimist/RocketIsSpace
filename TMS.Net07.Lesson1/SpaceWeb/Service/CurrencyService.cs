﻿using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Globalization;
using AutoMapper;

namespace SpaceWeb.Service
{
    public class CurrencyService : ICurrencyService
    {
        private UserService _userService;
        private ExchangeRateToUsdCurrentRepository _exchangeRateToUsdCurrentRepository;
        private ExchangeAccountHistoryRepository _exchangeAccountHistoryRepository;
        private ExchangeRateToUsdHistoryRepository _exchangeRateToUsdHistoryRepository;
        private IMapper _mapper;

        public CurrencyService(UserService userService,
            ExchangeRateToUsdCurrentRepository exchangeRateToUsdCurrentRepository,
            ExchangeAccountHistoryRepository exchangeAccountHistoryRepository,
            ExchangeRateToUsdHistoryRepository exchangeRateToUsdHistoryRepository,
            IMapper mapper)
        {
            _userService = userService;
            _exchangeRateToUsdCurrentRepository = exchangeRateToUsdCurrentRepository;
            _exchangeAccountHistoryRepository = exchangeAccountHistoryRepository;
            _exchangeRateToUsdHistoryRepository = exchangeRateToUsdHistoryRepository;
            _mapper = mapper;
        }

        public decimal ConvertByAlex(decimal amount, Currency currencyTo)
        {
            var user = _userService.GetCurrent();
            var currencyFrom = Currency.USD;
            var typeOfExchange = TypeOfExchange.Sell;
            var exchangeRate = _exchangeRateToUsdCurrentRepository.GetExchangeRate(currencyTo, typeOfExchange).ExchRate;

            var convertedAmount = amount * exchangeRate;

            PutToExchangeAccountHistory(currencyFrom, currencyTo, typeOfExchange, exchangeRate, amount, user);

            return convertedAmount;
        }

        public decimal ConvertByAlex(Currency currencyFrom, decimal amount, Currency currencyTo)
        {
            var user = _userService.GetCurrent();
            var typeOfExchangeToUsd = TypeOfExchange.Buy;
            var typeOfExchangeUsdToCurrencyFrom = TypeOfExchange.Sell;
            var exchRateToUsd = _exchangeRateToUsdCurrentRepository
                .GetExchangeRate(currencyFrom, typeOfExchangeToUsd).ExchRate;
            var exchRateUsdToCurrencyFrom = _exchangeRateToUsdCurrentRepository
                .GetExchangeRate(currencyTo, typeOfExchangeUsdToCurrencyFrom).ExchRate;

            var convertedAmount = amount / exchRateToUsd * exchRateUsdToCurrencyFrom;

            PutToExchangeAccountHistory(currencyFrom, currencyTo, typeOfExchangeUsdToCurrencyFrom,
                exchRateUsdToCurrencyFrom, amount, user);

            return convertedAmount;
        }

        public void PutToExchangeAccountHistory(Currency currencyFrom, Currency currencyTo, TypeOfExchange typeOfExch,
            decimal exchRate, decimal amount, User owner)
        {
            var exchangeAccountHistoryDb = new ExchangeAccountHistory
            {
                CurrencyFrom = currencyFrom,
                CurrencyTo = currencyTo,
                TypeOfExch = typeOfExch,
                ExchRate = exchRate,
                Amount = amount,
                ExchDate = DateTime.Now,
                Owner = owner
            };
            _exchangeAccountHistoryRepository.Save(exchangeAccountHistoryDb);
        }

        public bool CheckBalanceToPay(BankAccount account, decimal amount)
        {
            return false;
        }

        public GottenCurrency GetExchangeRates()
        {
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create("https://belarusbank.by/api/kursExchange?city=Минск");
            WebResponse response = request.GetResponse();

            List<GottenCurrency> fin = null;

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var cleanJson = reader.ReadToEnd();
                    fin = JsonSerializer.Deserialize<List<GottenCurrency>>(cleanJson);
                }
            }
            response.Close();

            var exchangeRates = fin[0];

            return exchangeRates;
        }

        public void PutCurrentExchangeRatesToDb(ExchangeRateToUsdCurrentRepository _exchangeRateToUsdCurrentRepository,
            GottenCurrency exchangeRates)
        {
            var exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = Currency.BYN,
                TypeOfExch = TypeOfExchange.Buy,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_in, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = Currency.BYN,
                TypeOfExch = TypeOfExchange.Sell,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_out, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = Currency.USD,
                TypeOfExch = TypeOfExchange.Buy,
                ExchRate = 1
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = Currency.USD,
                TypeOfExch = TypeOfExchange.Sell,
                ExchRate = 1
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = Currency.EUR,
                TypeOfExch = TypeOfExchange.Buy,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_in, new CultureInfo("en-US"))
                    / Convert.ToDecimal(exchangeRates.EUR_in, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = Currency.EUR,
                TypeOfExch = TypeOfExchange.Sell,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_out, new CultureInfo("en-US"))
                / Convert.ToDecimal(exchangeRates.EUR_out, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = Currency.GBP,
                TypeOfExch = TypeOfExchange.Buy,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_in, new CultureInfo("en-US"))
                / Convert.ToDecimal(exchangeRates.GBP_in, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = Currency.GBP,
                TypeOfExch = TypeOfExchange.Sell,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_out, new CultureInfo("en-US"))
                / Convert.ToDecimal(exchangeRates.GBP_out, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = Currency.PLN,
                TypeOfExch = TypeOfExchange.Buy,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_in, new CultureInfo("en-US"))
                / Convert.ToDecimal(exchangeRates.PLN_in, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = Currency.PLN,
                TypeOfExch = TypeOfExchange.Sell,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_out, new CultureInfo("en-US"))
                / Convert.ToDecimal(exchangeRates.PLN_out, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            Console.WriteLine();
        }

        public void DeleteCurrentExchRatesFromDb(ExchangeRateToUsdCurrentRepository _exchangeRateToUsdCurrentRepository)
        {
            var exchRates = _exchangeRateToUsdCurrentRepository.GetAll();

            foreach (var rate in exchRates)
            {
                _exchangeRateToUsdCurrentRepository.Remove(rate.Id);
            }
        }

        public void MoveCurrentExchangesDbToHistoryDb(ExchangeRateToUsdCurrentRepository _exchangeRateToUsdCurrentRepository,
            ExchangeRateToUsdHistoryRepository _exchangeRateToUsdHistoryRepository)
        {
            var exchCurrentRates = _exchangeRateToUsdCurrentRepository.GetAll();
            var exchRateHistory = new ExchangeRateToUsdHistory();

            foreach (var exchCurrRate in exchCurrentRates)
            {
                exchRateHistory = new ExchangeRateToUsdHistory
                {
                    Currency = exchCurrRate.Currency,
                    TypeOfExch = exchCurrRate.TypeOfExch,
                    ExchRate = exchCurrRate.ExchRate,
                    ExchRateDate = DateTime.Now
                };
                _exchangeRateToUsdHistoryRepository.Save(exchRateHistory);
            }
        }
    }
}

public class GottenCurrency
{
    public string USD_in { get; set; }
    public string USD_out { get; set; }
    public string EUR_in { get; set; }
    public string EUR_out { get; set; }
    public string GBP_in { get; set; }
    public string GBP_out { get; set; }
    public string PLN_in { get; set; }
    public string PLN_out { get; set; }
}
