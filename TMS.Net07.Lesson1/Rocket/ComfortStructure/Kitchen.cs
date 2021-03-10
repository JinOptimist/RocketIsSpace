using System;

namespace Rocket.ComfortStructure
{
    class Kitchen : IComfortStructure
    {
        private int _storageCapacity;
        private int _seatsNumber;

        public Kitchen(double mass, int storageCapacity, int seatsNumber)
        {
            if (mass > 0 &&
                storageCapacity > 0 && storageCapacity <= 20 &&
                seatsNumber > 0 && seatsNumber <= 10)
            {
                Mass = mass;
                _storageCapacity = storageCapacity;
                _seatsNumber = seatsNumber;
            }
            else
            {
                throw new Exception("Wrong data. Expected: " +
                                    "mass > 0, " +
                                    "0 < storage capacity < 20, " +
                                    "0 < seats number < 10");
            }
        }

        public double Mass { get; }

        public string GetInfo()
        {
            return $"Kitchen mass: {Mass} tons" +
                   $"{Environment.NewLine}Storage capacity: {_storageCapacity}" +
                   $"{Environment.NewLine}Seats number: {_seatsNumber}";
        }
    }
}