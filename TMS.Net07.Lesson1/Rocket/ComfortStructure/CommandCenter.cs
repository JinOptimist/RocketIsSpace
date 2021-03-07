using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.ComfortStructure
{
    class CommandCenter : IComfortStructure
    {
        public CommandCenter(double weight)
        {
            if (weight > 0)
            {
                Weight = weight;
            }
            else
            {
                throw new Exception("Wrong weight. Expected: weight > 0");
            }
        }
        public double Weight { get; }

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
            return $"Command center weight: {Weight}";
        }
    }
}
