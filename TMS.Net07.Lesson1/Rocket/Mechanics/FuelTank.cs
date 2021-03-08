using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Mechanics
{
    public class FuelTank
    {
        public double FuelTankWeight { get; private set; }
        public double FuelTankCapacity { get; private set; }
        public FuelTank(double fuelTankWeight, double fuelTankCapacity)
        {
            FuelTankWeight = fuelTankWeight;
            FuelTankCapacity = fuelTankCapacity;
        }
        public string GetInfo()
        {
            return $"Fuel tank weight = {FuelTankWeight} kg." +
                $"{Environment.NewLine}Fuel tank capacity = {FuelTankCapacity} kg.";
        }
    }
}

