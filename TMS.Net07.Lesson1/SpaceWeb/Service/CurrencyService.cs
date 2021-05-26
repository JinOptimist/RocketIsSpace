using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;
using System.Net;

namespace SpaceWeb.Service
{
    public class CurrencyService : ICurrencyService
    {
        public const string DolarCode = "145";
        public decimal ConvertAmount( Currency currency)
        {
            WebClient client = new WebClient();
            string url = "https://www.nbrb.by/services/xmlexrates.aspx";
            var xml = client.DownloadString(url);
            XDocument xdoc = XDocument.Parse(xml);
            var allCurrencyInfo = xdoc.Element("DailyExRates").Elements("Currency");

            switch (currency)
            {
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
    }
    
}
