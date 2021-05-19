using System;
using System.IO;
using System.Net;

namespace RocketSMS
{
    public class Class1
    {
        static String Username = "193197322";
        static String Password = "aA8M8yAdl9sMs";

        public static void Main(string[] args)
        {
            sendSMS("375296890043", "console app");
        }

        public static Boolean sendSMS(String to, String text)
        {
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create("http://api.rocketsms.by/simple/send?username=" + Username + "&password=" + Password + "&phone=" + Uri.EscapeUriString(to) + "&text=" + Uri.EscapeUriString(text) + "&priority=true");
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
