using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Mechanics
{
    public class RocketStages
    {
        public RocketStages(List<Engines> engines, List<FuelTanks> fuelTanks)
        {
            Engines = engines;
            FuelTanks = fuelTanks;
        }
        public List<Engines> Engines { get; set; }
        public List<FuelTanks> FuelTanks { get; set; }
        public double StageWeight
        {
            get => GetStageWeight();
            private set { }
        }
        public double StageFuelConsumption
        {
            get => GetStageFuelConsumption();
            private set { }
        }
        public double StageFuelTanksCapacity
        {
            get => GetStageFuelTanksCapacity();
            private set { }
        }
        public double GetStageWeight()
        {
            double stageWeight = Engines.
                 Sum(engines => engines.EngineWeight)
                 + FuelTanks.
                 Sum(fuelTanks => fuelTanks.FuelTankWeight);
            return stageWeight;
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
            var inSecondFuelConsumption = StageFuelTanksCapacity - StageFuelConsumption;

            while (inSecondFuelConsumption! <= 0)
            {
                inSecondFuelConsumption -= StageFuelConsumption;
            }
            return "Fuel tanks are empty. Stage has been detached.";
        }
        public string GetInfo()
        {
            return $"On this stage installed {Engines.Count()} engines and {FuelTanks.Count()} fuel tanks." +
                $"{Environment.NewLine}Stage weight is {StageWeight} kg." +
                $"{Environment.NewLine}Stage fuel tanks capacity is {StageFuelTanksCapacity} kg" +
                $"{Environment.NewLine}Stage fuel consumption is {StageFuelConsumption} kg/sec";
        }
    }
}
