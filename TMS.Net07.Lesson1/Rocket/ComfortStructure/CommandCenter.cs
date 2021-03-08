using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void GetSignal(string signal)
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
            return $"Command center mass: {Mass}";
        }
    }
}