using System;
using System.Diagnostics;
using ExchangeRate;

namespace ExchangeCurrencyLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            SetDefaultExchangeRates();
        }

        public static void SetDefaultExchangeRates()
        {
            ExchangeRate.Program.LaunchPuttingExchRatesToDb();

            Console.WriteLine("");
        }
    }
}
