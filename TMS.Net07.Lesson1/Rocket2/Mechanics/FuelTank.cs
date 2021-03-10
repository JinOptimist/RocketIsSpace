using System;

namespace Rocket2.Mechanics
{
    public class FuelTank
    {
        public double FuelTankMass { get; private set; }
        public double FuelTankCapacity { get; private set; }
        public FuelTank(double fuelTankMass, double fuelTankCapacity)
        {
            FuelTankMass = fuelTankMass;
            FuelTankCapacity = fuelTankCapacity;
        }
        public string GetInfo()
        {
            return $"Fuel tank mass: {FuelTankMass} tons" +
                   $"{Environment.NewLine}Fuel tank capacity: {FuelTankCapacity} tons";
        }
    }
}

