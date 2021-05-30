using System;
using System.Diagnostics;
using ExchangeRate;

namespace ExchangeCurrencyLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public static void SetDefaultExchangeRates()
        {
            ExchangeRate.Program.LaunchPuttingExchRatesToDb();

            Console.WriteLine("");
        }
    }
}
