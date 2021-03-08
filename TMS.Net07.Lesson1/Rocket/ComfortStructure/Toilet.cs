using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.ComfortStructure
{
    public class Toilet : IComfortStructure
    {
        private bool _isOpen = true;

        public Toilet(double weight)
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

        public void OpenToilet() => _isOpen = true;
        public void CloseToilet() => _isOpen = false;

        public double Weight { get; }

        public string GetInfo()
        {
            return $"Toilet weight: {Weight}";
        }
    }
}