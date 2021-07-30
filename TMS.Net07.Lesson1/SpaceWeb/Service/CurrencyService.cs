using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.Json;
using AutoMapper;

namespace SpaceWeb.Service
{
    public class CurrencyService : ICurrencyService
    {
        private IUserService _userService;
        private IExchangeRateToUsdCurrentRepository _exchangeRateToUsdCurrentRepository;
        private IExchangeAccountHistoryRepository _exchangeAccountHistoryRepository;
        private IExchangeRateToUsdHistoryRepository _exchangeRateToUsdHistoryRepository;
        private IMapper _mapper;

        public CurrencyService(IUserService userService,
            IExchangeRateToUsdCurrentRepository exchangeRateToUsdCurrentRepository,
            IExchangeAccountHistoryRepository exchangeAccountHistoryRepository,
            IExchangeRateToUsdHistoryRepository exchangeRateToUsdHistoryRepository,
            IMapper mapper)
        {
            _userService = userService;
            _exchangeRateToUsdCurrentRepository = exchangeRateToUsdCurrentRepository;
            _exchangeAccountHistoryRepository = exchangeAccountHistoryRepository;
            _exchangeRateToUsdHistoryRepository = exchangeRateToUsdHistoryRepository;
            _mapper = mapper;
        }


        public const string DolarCode = "145";
        public decimal ConvertAmount(Currency currency)
        {
            WebClient client = new WebClient();
            string url = "https://www.nbrb.by/services/xmlexrates.aspx";
            var xml = client.DownloadString(url);
            XDocument xdoc = XDocument.Parse(xml);
            var allCurrencyInfo = xdoc.Element("DailyExRates").Elements("Currency");

            switch (currency)
            {
                case Currency.BYN:
                    return 1;
                case Currency.USD:
                    return Convert.ToDecimal(allCurrencyInfo
                        .Where(x => x.Attribute("Id").Value == DolarCode)
                        .Select(x => x.Element("Rate").Value)
                        .FirstOrDefault(), CultureInfo.InvariantCulture);
                case Currency.EUR:
                    return Convert.ToDecimal(allCurrencyInfo
                        .Where(x => x.Attribute("Id").Value == "292")
                        .Select(x => x.Element("Rate").Value)
                        .FirstOrDefault(), CultureInfo.InvariantCulture);
                default:
                    throw new Exception("Не верная валюта");
            }
        }

        public decimal GetExchangeRate(Currency currencyFrom, decimal amount, Currency currencyTo)
        {
            //Convert Euro to Euro
            if (currencyFrom == currencyTo)
                return amount;
            var toRate = ConvertAmount(currencyTo);
            var fromRate = ConvertAmount(currencyFrom);

            return ((amount * toRate) / fromRate);
        }
        public bool IsCardAvailability(EnumBankCard Card)
        {
            var allCardTypes = _userService
                .GetCurrent()
                .BankAccounts
                .SelectMany(x => x.BanksCards)
                .Select(x => x.Card)
                .ToList();

            return !allCardTypes.Any(x=>x == Card);
        }
        public string IntToStringAmount(int amount)
        {
            int[] array_int = new int[4];
            string[,] array_string = new string[4, 3]
            {
                {"миллиард", "миллиарда", "миллиардов" },
                {"миллион", "миллиона", "миллионов" },
                {"тысяча", "тысячи", "тысяч" },
                {"", "", "" }
            };
            array_int[0] = (amount - (amount % 1000000000)) / 1000000000;
            array_int[1] = ((amount % 1000000000) - (amount % 1000000)) / 1000000;
            array_int[2] = ((amount % 1000000) - (amount % 1000)) / 1000;
            array_int[3] = amount % 1000;

            string result = "";

            for (int i = 0; i < 4; i++)
            {
                if (array_int[i] != 0)
                {
                    if (((array_int[i] - (array_int[i] % 100)) / 100) != 0)
                        switch (((array_int[i] - (array_int[i] % 100)) / 100))
                        {
                            case 1:
                                result += "сто";
                                break;
                            case 2:
                                result += "двести";
                                break;
                            case 3:
                                result += "триста";
                                break;
                            case 4:
                                result += "четыреста";
                                break;
                            case 5:
                                result += "пятьсот";
                                break;
                            case 6:
                                result += "шестьсот";
                                break;
                            case 7:
                                result += "семьсот";
                                break;
                            case 8:
                                result += "восемьсот";
                                break;
                            case 9:
                                result += "девятьсот";
                                break;
                        }
                    if (((array_int[i] % 100) - ((array_int[i] % 100) % 10)) / 10 != 1)
                    {
                        switch (((array_int[i] % 100) - ((array_int[i] % 100) % 10)) / 10)
                        {
                            case 2: result += " двадцать"; break;
                            case 3: result += " тридцать"; break;
                            case 4: result += " сорок"; break;
                            case 5: result += " пятьдесят"; break;
                            case 6: result += " шестьдесят"; break;
                            case 7: result += " семьдесят"; break;
                            case 8: result += " восемьдесят"; break;
                            case 9: result += " девяносто"; break;
                        }
                    }
                    switch (array_int[i] % 10)
                    {
                        case 1: if (i == 2) result += "одна"; else result += "один"; break;
                        case 2: if (i == 2) result += " две"; else result += " два"; break;
                        case 3: result += " три"; break;
                        case 4: result += " четыре"; break;
                        case 5: result += " пять"; break;
                        case 6: result += " шесть"; break;
                        case 7: result += " семь"; break;
                        case 8: result += " восемь"; break;
                        case 9: result += " девять"; break;

                    }
                }
                else switch (array_int[i] % 100)
                    {
                        case 10: result += " десять"; break;
                        case 11: result += " одиннадцать"; break;
                        case 12: result += " двенадцать"; break;
                        case 13: result += " тринадцать"; break;
                        case 14: result += " четырнадцать"; break;
                        case 15: result += " пятнадцать"; break;
                        case 16: result += " шестнадцать"; break;
                        case 17: result += " семнадцать"; break;
                        case 18: result += " восемннадцать"; break;
                        case 19: result += " девятнадцать"; break;
                    }
                if (array_int[i] % 100 >= 10 && array_int[i] % 100 <= 19) result += " " + array_string[i, 2] + " ";
                else switch (array_int[i] % 10)
                    {
                        case 1: result += " " + array_string[i, 0] + " "; break;
                        case 2:
                        case 3:
                        case 4: result += " " + array_string[i, 1] + " "; break;
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9: result += " " + array_string[i, 2] + " "; break;
                    }
            }
            return result;
        }
        public decimal ConvertByAlex(decimal amount, Currency currencyTo)
        {
            var user = _userService.GetCurrent();
            var currencyFrom = Currency.USD;
            var typeOfExchange = TypeOfExchange.Sell;
            var exchangeRate = _exchangeRateToUsdCurrentRepository
                .GetExchangeRate(currencyTo, typeOfExchange).ExchRate;

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
            return (account.Amount >= amount) ? true : false;
        }

        public GottenCurrency GetExchangeRates()
        {
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create("https://belarusbank.by/api/kursExchange?city=Минск");
            WebResponse response = request?.GetResponse();

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
                * 10
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = Currency.PLN,
                TypeOfExch = TypeOfExchange.Sell,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_out, new CultureInfo("en-US"))
                / Convert.ToDecimal(exchangeRates.PLN_out, new CultureInfo("en-US"))
                * 10
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
            ExchangeRateToUsdHistoryRepository _exchangeRateToUsdHistoryRepository, Mapper _mapper)
        {
            var exchCurrentRates = _exchangeRateToUsdCurrentRepository.GetAll();

            foreach (var exchCurrRate in exchCurrentRates)
            {
                //var exchRateHistory = _mapper.Map<ExchangeRateToUsdHistory>(exchCurrRate);
                //exchRateHistory.ExchRateDate = DateTime.Now;
                //_exchangeRateToUsdHistoryRepository.Save(exchRateHistory);

                var exchRateHistory = new ExchangeRateToUsdHistory
                {
                    Currency = exchCurrRate.Currency,
                    TypeOfExch = exchCurrRate.TypeOfExch,
                    ExchRate = exchCurrRate.ExchRate,
                    ExchRateDate = GetDateWithNullSecAndMillisec()
                };
                _exchangeRateToUsdHistoryRepository.Save(exchRateHistory);
            }
        }

        /// <summary>
        /// Method for getting Date with null seconds and milliseconds. Example: 04.06.2021 13:32:00.000
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateWithNullSecAndMillisec()
        {
            var time = DateTime.Now;
            time = time.AddSeconds(-time.Second);
            time = time.AddMilliseconds(-time.Millisecond);

            return time;
        }

        public decimal CountAllMoneyInWishingCurrency(List<BankAccount> accounts, Currency currencyTo)
        {
            decimal amountAllMoneyInDefaultCurrency = 0;

            foreach (var account in accounts)
            {
                var amount = ConvertByAlex(account.Currency, account.Amount, currencyTo);
                amountAllMoneyInDefaultCurrency += amount;
            }

            return Math.Round(amountAllMoneyInDefaultCurrency, 2);
        }

        public bool CheckInternetConnection()
        {
            WebClient client = new WebClient();
            try
            {
                using (client.OpenRead("http://www.google.com"))
                {
                }
                return true;
            }
            catch (WebException)
            {
                return false;
            }

            return true;
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

