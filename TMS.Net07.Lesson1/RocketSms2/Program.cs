

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpaceWeb.EfStuff;
using SpaceWeb.EfStuff.Repositories;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace RocketSms2
{
    public class MySettings
    {
        public string Connection { get; set; }
    }
    class Program
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
            string username = "193197322";
            string password = "aA8M8yAdl9sMs";
            string tel = "375293525980";
            string text = "console app";

            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            var connection = dbContextOptionsBuilder
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SpaceWeb;Trusted_Connection=True");

            SpaceDbContext spaceDbContext = new SpaceDbContext(connection.Options);
            InsuranceTypeRepository insuranceTypeRepository = new InsuranceTypeRepository(spaceDbContext);
            InsuranceRepository insuranceRepository = new InsuranceRepository(spaceDbContext);

            //SendSMS(tel, text, username, password);
            //CheckDb(insuranceTypeRepository, insuranceRepository);

            //CheckDatabase test = new CheckDatabase();
            CheckDb(insuranceTypeRepository, insuranceRepository);
        }

        public static void CheckDb(InsuranceTypeRepository _insuranceTypeRepository, InsuranceRepository _insuranceRepository)
        {
            var models = _insuranceRepository
                .GetAll()
                .ToList();
            Console.WriteLine();
        }

        public static void SendSMS(string tel, string text, string username, string password)
        {
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create("http://api.rocketsms.by/simple/send?username=" + username + "&password=" + password + "&phone=" + Uri.EscapeUriString(tel) + "&text=" + Uri.EscapeUriString(text));
            WebResponse response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    using (var jsonTextReader = new JsonTextReader(reader))
                    {
                        Deser deser = new Deser();
                        var serializer = new Newtonsoft.Json.JsonSerializer();
                        var aa = serializer.Deserialize(jsonTextReader);
                        deser = serializer.Deserialize<Deser>(jsonTextReader);
                        Deser.Print(deser);
                        //Console.WriteLine(aa);
                        //Console.WriteLine(aa.ToString());
                        Console.ReadKey();
                    }
                }
            }
            response.Close();


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

    public class Deser
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public Cost Cost { get; set; }
        public int Credits { get; set; }
        public decimal Money { get; set; }
        //public decimal money { get; set; }
        //public int countMessage = 0;
        //public double money = 0;

        public static void Print(Deser deser)
        {
            Console.WriteLine(deser.Id);
            Console.WriteLine(deser.Status);
            Console.WriteLine(deser.Credits);
            Console.WriteLine(deser.Money);
            //Console.WriteLine(deser.money);
            //Console.WriteLine(deser.cost[1]);
        }
    }

    public class Cost
    {

    }
}