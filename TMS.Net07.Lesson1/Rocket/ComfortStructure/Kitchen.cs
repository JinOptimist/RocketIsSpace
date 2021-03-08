using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.ComfortStructure
{
    class Kitchen : IComfortStructure
    {
        private int _storageCapacity;
        private int _seatsNumber;
        public Kitchen(double weight, int storageCapacity, int seatsNumber)
        {
            if (weight > 0 &&
                storageCapacity > 0 && storageCapacity <= 20 &&
                seatsNumber > 0 && seatsNumber <= 10)
            {
                Weight = weight;
                _storageCapacity = storageCapacity;
                _seatsNumber = seatsNumber;
            }
            else
            {
                throw new Exception("Wrong data. Expected: " +
                    "weight > 0, " +
                    "0 < storage capacity < 20, " +
                    "0 < seats number < 10");
            }
        }
        public double Weight { get; }

        public string GetInfo()
        {
            return $"Kitchen weight: {Weight}\n" +
                $"Storage capacity: {_storageCapacity}\n" +
                $"Seats number: {_seatsNumber}";
        }
    }
}
