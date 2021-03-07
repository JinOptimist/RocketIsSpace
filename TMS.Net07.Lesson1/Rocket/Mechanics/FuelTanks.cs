using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Mechanics
{
    public class FuelTanks
    {
        public double fuelTankWeight { get; private set; }
        public double fuelTankCapacity { get; private set; }

        public FuelTanks(double FuelTankWeight, double FuelTankCapacity)
        {
            FuelTankWeight = fuelTankWeight;
            FuelTankCapacity = fuelTankCapacity;
        }
        public double GetFuelTankWeight()
        {
            return fuelTankWeight;
        }
        public double GetFuelTankCapacity()
        {
            return fuelTankCapacity;
        }
        public string GetInfo()
        {
            return $"Fuel tank weight = {this.fuelTankWeight} kg." +
                $"Fuel tank capacity = {this.fuelTankCapacity} kg.";
        }
    }
}

