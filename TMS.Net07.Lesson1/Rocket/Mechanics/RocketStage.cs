using System;
using System.Collections.Generic;
using System.Linq;

namespace Rocket.Mechanics
{
    public class RocketStage : IRocket
    {
        private double _stageMass;
        private double _fuelConsumption;
        private double _fuelTanksCapacity;

        public RocketStage(List<Engine> engines, List<FuelTank> fuelTank)
        {
            Engines = engines;
            FuelTanks = fuelTank;
            _stageMass = GetStageMass();
            _fuelConsumption = GetStageFuelConsumption();
            _fuelTanksCapacity = GetStageFuelTanksCapacity();
        }

        public List<Engine> Engines { get; }
        public List<FuelTank> FuelTanks { get; }

        public double GetStageMass()
        {
            double stageMass = Engines.Sum(engines => engines.EngineMass)
                               + FuelTanks.Sum(fuelTanks => fuelTanks.FuelTankMass);
            return stageMass;
        }

        private double GetStageFuelConsumption()
        {
            double stageFuelConsumption = Engines.Sum(engines => engines.EngineFuelConsumption);
            return stageFuelConsumption;
        }

        private double GetStageFuelTanksCapacity()
        {
            double stageFuelTanksCapacity = FuelTanks.Sum(fuelTanks => fuelTanks.FuelTankCapacity);
            return stageFuelTanksCapacity;
        }

        public string StageDetaching()
        {
            var inSecondFuelConsumption = GetStageFuelTanksCapacity() - GetStageFuelConsumption();

            while (inSecondFuelConsumption! <= 0)
            {
                inSecondFuelConsumption -= GetStageFuelConsumption();
            }

            return "Fuel tanks are empty. Stage has been detached.";
        }

        public string GetInfo()
        {
            return $"On this stage installed {Engines.Count()} engines and {FuelTanks.Count()} fuel tanks." +
                   $"{Environment.NewLine}Stage mass is {_stageMass} kg." +
                   $"{Environment.NewLine}Stage fuel tanks capacity is {_fuelTanksCapacity} kg" +
                   $"{Environment.NewLine}Stage fuel consumption is {_fuelConsumption} kg/sec";
        }
    }
}
