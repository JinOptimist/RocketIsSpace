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
    public class SmsService : ISmsService
    {
        private const string USERNAME = "193197322";
        private const string PASSWORD = "aA8M8yAdl9sMs";

        public string ConvertToDefaultPhoneNumber(string phone)
        {
            phone = phone.Trim(new Char[] { ' ', '+' });

            for (int i = 0; i < phone.Length; i++)
            {
                if (phone[i] == ' ')
                {
                    phone = phone.Remove(i--, 1);
                }
            }

            if (phone[0] == '8' && phone[1] == '0')
            {
                phone = phone.Remove(0, 2);
                phone = phone.Insert(0, "375");
            }

            return phone;
        }

        public int CreateCodeFromSms()
        {
            var random = new Random();
            int value = random.Next(1000, 9999);

            return value;
        }

        public void SendSMS(string tel, string text)
        {
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create("http://api.rocketsms.by/simple/send?username=" + USERNAME + "&password=" + PASSWORD + "&phone=" + Uri.EscapeUriString(tel) + "&text=" + Uri.EscapeUriString(text) + "&priority=" + true);
            WebResponse response = request.GetResponse();

            response.Close();
        }
    }

    public class Deser
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public static void Print(Deser deser)
        {
            Console.WriteLine(deser.Id);
            Console.WriteLine(deser.Status);
        }
    }
}