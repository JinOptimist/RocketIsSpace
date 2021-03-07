
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HumansResources.Humans.Persons
{
    /*class PhoneNumber
    {
        private string _PhoneNumber;

       
        public string GetPnoneNumber() {
            return _PhoneNumber;
        }

        public PhoneNumber Parse(string stringPnoneNumber, out PhoneNumber result)
        {
            result = this;
            return result;
        }
    }*/

    class PhoneNumber
    {
        [JsonPropertyName("valid")]
        public bool IsValid { get; }

        [JsonPropertyName("number")]
        public string PhoneNumberInput { get; }

        [JsonPropertyName("e164Format")]
        public string PhoneNumberE164 { get; }

        [JsonPropertyName("internationalFormat")]
        public string PhoneNumberInternational { get; }

        [JsonPropertyName("nationalFormatt")]
        public string PhoneNumberNational { get; }

        [JsonPropertyName("countryCode")]
        public string CountryCode { get; }

        [JsonPropertyName("countryPrefix")]
        public string CountryPrefix { get; }

        [JsonPropertyName("countryName")]
        public string CountryName { get; }


        public PhoneNumber()
        {
        }

        public PhoneNumber(string phoneNumberInput)
        {
            PhoneNumberInput = phoneNumberInput;
            ParseFromAPI();
        }


        public void ParseFromAPI()
        {

            //https://numvalidate.com/numvalidate-docs/index.html?java#authentication
            string jsonPhoneNumber = "";
            string url = $"https://numvalidate.com/api/validate?number={PhoneNumberInput}";
            /*Console.WriteLine(url);
            WebRequest request = WebRequest.Create(url);
            try
            {
                using WebResponse response = request.GetResponse();
                using Stream stream = response.GetResponseStream();
                using StreamReader streamReader = new StreamReader(stream);
                string line = "";
                StringBuilder builder = new StringBuilder();
                while ((line = streamReader.ReadLine()) != null)
                {
                    builder.AppendLine(line);
                }
                jsonPhoneNumber = builder.ToString();

            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
                return;
            }*/



            //string url = @"https://api.stackexchange.com/2.2/answers?order=desc&sort=activity&site=stackoverflow";
            //https://numverify.com/s
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonPhoneNumber = reader.ReadToEnd();
            }






            PhoneNumber pnoneNumber = null;
            try
            {
                pnoneNumber = JsonSerializer.Deserialize<PhoneNumber>(jsonPhoneNumber);
            }
            catch (JsonException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            Console.WriteLine(pnoneNumber.IsValid);
            Console.WriteLine(pnoneNumber.CountryName);
            Console.WriteLine(pnoneNumber.PhoneNumberInternational);
        }
    }
}

   


/*{
    "data": {
    "valid": true,
    "number": "12015550123",
    "e164Format": "+12015550123",
    "internationalFormat": "+1 201-555-0123",
    "nationalFormat": "(201) 555-0123",
    "countryCode": "US",
    "countryPrefix": "1",
    "countryName": "United States"
    }
}*/