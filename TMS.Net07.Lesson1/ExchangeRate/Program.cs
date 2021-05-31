

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
using SpaceWeb.EfStuff;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Xml.Serialization;

namespace ExchangeRate
{
    public class MySettings
    {
        public string Connection { get; set; }
    }
    public class Program
    {
        //{
        //  "id": 141664774,
        //  "status": "QUEUED",
        //  "cost": {
        //    "credits": 1,
        //    "money": 0.0295
        //  }
        //}
        static void Main(string[] args)
        {
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            var connection = dbContextOptionsBuilder
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SpaceWeb;Trusted_Connection=True");

            SpaceDbContext spaceDbContext = new SpaceDbContext(connection.Options);
            ExchangeRateToUsdCurrentRepository exchangeRateToUsdCurrentRepository =
                new ExchangeRateToUsdCurrentRepository(spaceDbContext);

            var currentDate = DateTime.Now;
            //while(true)
            //{
                //if (((currentDate.Minute / 10) == 1) | ((currentDate.Minute / 11) == 1))
                //{
                    var exchangeRates = GetExchangeRates();
                    PutCurrentExchangeRatesToDb(exchangeRateToUsdCurrentRepository, exchangeRates);
                    //currentDate = DateTime.Now;
                //}
            //}

            

        }

        public static void LaunchPuttingExchRatesToDb()
        {
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            var connection = dbContextOptionsBuilder
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SpaceWeb;Trusted_Connection=True");

            SpaceDbContext spaceDbContext = new SpaceDbContext(connection.Options);
            ExchangeRateToUsdCurrentRepository exchangeRateToUsdCurrentRepository =
                new ExchangeRateToUsdCurrentRepository(spaceDbContext);



            var currentDate = DateTime.Now;

            var exchangeRates = GetExchangeRates();
            PutCurrentExchangeRatesToDb(exchangeRateToUsdCurrentRepository, exchangeRates);
        }

        public static void PutCurrentExchangeRatesToDb(ExchangeRateToUsdCurrentRepository _exchangeRateToUsdCurrentRepository, Currency exchangeRates)
        {
            var exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = SpaceWeb.Models.Currency.BYN,
                TypeOfExch = SpaceWeb.Models.TypeOfExchange.Buy,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_in, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = SpaceWeb.Models.Currency.BYN,
                TypeOfExch = SpaceWeb.Models.TypeOfExchange.Sell,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_out, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = SpaceWeb.Models.Currency.USD,
                TypeOfExch = SpaceWeb.Models.TypeOfExchange.Buy,
                ExchRate = 1
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = SpaceWeb.Models.Currency.USD,
                TypeOfExch = SpaceWeb.Models.TypeOfExchange.Sell,
                ExchRate = 1
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = SpaceWeb.Models.Currency.EUR,
                TypeOfExch = SpaceWeb.Models.TypeOfExchange.Buy,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_in, new CultureInfo("en-US"))
                    / Convert.ToDecimal(exchangeRates.EUR_in, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = SpaceWeb.Models.Currency.EUR,
                TypeOfExch = SpaceWeb.Models.TypeOfExchange.Sell,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_out, new CultureInfo("en-US")) 
                / Convert.ToDecimal(exchangeRates.EUR_out, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = SpaceWeb.Models.Currency.GBP,
                TypeOfExch = SpaceWeb.Models.TypeOfExchange.Buy,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_in, new CultureInfo("en-US")) 
                / Convert.ToDecimal(exchangeRates.GBP_in, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = SpaceWeb.Models.Currency.GBP,
                TypeOfExch = SpaceWeb.Models.TypeOfExchange.Sell,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_out, new CultureInfo("en-US")) 
                / Convert.ToDecimal(exchangeRates.GBP_out, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = SpaceWeb.Models.Currency.PLN,
                TypeOfExch = SpaceWeb.Models.TypeOfExchange.Buy,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_in, new CultureInfo("en-US")) 
                / Convert.ToDecimal(exchangeRates.PLN_in, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            exchangeRateDb = new ExchangeRateToUsdCurrent
            {
                Currency = SpaceWeb.Models.Currency.PLN,
                TypeOfExch = SpaceWeb.Models.TypeOfExchange.Sell,
                ExchRate = Convert.ToDecimal(exchangeRates.USD_out, new CultureInfo("en-US")) 
                / Convert.ToDecimal(exchangeRates.PLN_out, new CultureInfo("en-US"))
            };
            _exchangeRateToUsdCurrentRepository.Save(exchangeRateDb);

            Console.WriteLine();
        }

        public static Currency GetExchangeRates()
        {
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create("https://belarusbank.by/api/kursExchange?city=Минск");
            WebResponse response = request.GetResponse();

            List<Currency> fin = null;

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var cleanJson = reader.ReadToEnd();
                    fin = JsonSerializer.Deserialize<List<Currency>>(cleanJson);
                }
            }
            response.Close();

            var exchangeRates = fin[0];

            return exchangeRates;
        }


    }

    public class Currency
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
}