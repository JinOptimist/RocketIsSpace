

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
using SpaceWeb.EfStuff;
using SpaceWeb.EfStuff.Repositories;
using System;
using System.Collections.Generic;
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
    class Program
    {
        const string USERNAME = "193197322";
        const string PASSWORD = "aA8M8yAdl9sMs";
        const string TEL = "375293525980";
        const string TEXT = "console app";

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
            //string username = "193197322";
            //string password = "aA8M8yAdl9sMs";
            //string tel = "375293525980";
            //string text = "console app";

            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            var connection = dbContextOptionsBuilder
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SpaceWeb;Trusted_Connection=True");

            SpaceDbContext spaceDbContext = new SpaceDbContext(connection.Options);
            InsuranceTypeRepository insuranceTypeRepository = new InsuranceTypeRepository(spaceDbContext);
            InsuranceRepository insuranceRepository = new InsuranceRepository(spaceDbContext);


            GetExchangeRates();

            //SendSMS(TEL, TEXT, USERNAME, PASSWORD);
            //CheckDb(insuranceTypeRepository, insuranceRepository);

            //CheckDatabase test = new CheckDatabase();
            //CheckDb(insuranceTypeRepository, insuranceRepository);
        }

        //public static void CheckDb(InsuranceTypeRepository _insuranceTypeRepository, InsuranceRepository _insuranceRepository)
        //{
        //    DateTime compareDate = new DateTime(2021, 05, 12);

        //    var models = _insuranceRepository
        //        .GetAll()
        //        .Where(x => (x.DateCreationing.Year == compareDate.Year) && (x.DateCreationing.Month == compareDate.Month)
        //            && (x.DateCreationing.Day == compareDate.Day))
        //        .ToList();

        //    foreach (var x in models)
        //    {
        //        SendSMS(x.Phone, TEXT, USERNAME, PASSWORD);
        //    }

        //    Console.WriteLine();
        //}

        public static void GetExchangeRates()
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


            /*try
            {
                using (WebResponse response = request.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                        Console.WriteLine(streamReader.ReadToEnd());
                    return false;
                }
            }*/
        }


    }

    public class OverCurrency
    {
        public Currency[] Currency { get; set; }
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

    public class Deser
    {
        public int Id { get; set; }
        public string Status { get; set; }
        //public Cost Cost { get; set; }
        //public int Credits { get; set; }
        //public decimal Money { get; set; }
        //public decimal money { get; set; }
        //public int countMessage = 0;
        //public double money = 0;

        public static void Print(Deser deser)
        {
            Console.WriteLine(deser.Id);
            Console.WriteLine(deser.Status);
            //Console.WriteLine(deser.Credits);
            //Console.WriteLine(deser.Money);
            //Console.WriteLine(deser.money);
            //Console.WriteLine(deser.cost[1]);
        }
    }

    public class Cost
    {

    }
}