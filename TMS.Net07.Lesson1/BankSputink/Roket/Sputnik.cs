using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSputink.Rocket
{
    public class Sputnik : ISputnik
    {
        public Sputnik(int mass, string name = null)
        {
            Mass = mass;
            Name = name;
        }

        public int Mass { get; private set; }
        public bool IsReadyToLaunch { get; set; }
        public string Name { get; private set; }

        public string Launch()
        {
            if (Mass > 10)
            {
                throw new Exception("to heavy");
            }
            if (Mass > 5)
            {
                return "Medium";
            }

            return "Light";
        }
    }
}
