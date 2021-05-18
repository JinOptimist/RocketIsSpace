using System;
using System.IO;
using System.Net;

namespace RocketSMS
{
    public class Class1
    {
        static String Username = "123456789";
        static String Password = "password";
        static String Sender = "SMS-INFO";

        public static void Main(string[] args)
        {
            sendSMS("375296890043", "HELLO, C#");
        }

        public static Boolean sendSMS(String to, String text)
        {
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create("http://api.rocketsms.by/simple/send?username=" + Username + "&password=" + Password + "&phone=" + Uri.EscapeUriString(to) + "&sender=" + Uri.EscapeUriString(Sender) + "&text=" + Uri.EscapeUriString(text));
            try
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
            }
        }
    }
}
