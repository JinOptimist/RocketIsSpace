using System;
using System.Collections.Generic;

namespace Rocket.ComfortStructure
{
    class CommandCenter : IComfortStructure
    {
        public CommandCenter(double mass)
        {
            if (mass > 0)
            {
                Mass = mass;
            }
            else
            {
                throw new Exception("Wrong mass. Expected: mass > 0");
            }
        }

        public double Mass { get; }

        public List<string> Ledger;

        public void AddSignal(string signal)
        {
            Ledger.Add(signal);
        }

        public void SendSignal()
        {
            Console.WriteLine("Enter your message: ");
            Ledger.Add(Console.ReadLine());
        }

        public string GetInfo()
        {
            return $"Command center mass: {Mass} tons";
        }
    }
}