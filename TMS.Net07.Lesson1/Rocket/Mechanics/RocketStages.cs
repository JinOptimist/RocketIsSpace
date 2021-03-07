using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Mechanics
{
    public class RocketStages : IRocketStages
    {
        public RocketStages(double stageWeight, List<Engines> engines, List<FuelTanks> fuelTanks, double stageFuelConsumption, double stageFuelTanksCapacity)
        {
            StageWeight = stageWeight;
            Engines = engines;
            FuelTanks = fuelTanks;
            StageFuelConsumption = stageFuelConsumption;
            StageFuelTanksCapacity = stageFuelTanksCapacity;
        }
        public List<Engines> Engines { get; set; }
        public List<FuelTanks> FuelTanks { get; set; }
        public double StageWeight { get; set; }
        public double StageFuelConsumption { get; set; }
        public double StageFuelTanksCapacity { get; set; }

        public double GetStageWeight(List<Engines> Engines, List<FuelTanks> FuelTanks)
        {
            StageWeight = Engines.
                Sum(engines => engines.GetEngineWeight())
                + FuelTanks.
                Sum(fuelTanks => fuelTanks.GetFuelTankWeight());

            return StageWeight;
        }
        public double GetStageFuelConsumption(List<Engines> Engines)
        {
            StageFuelConsumption = Engines.Sum(engines => engines.GetEngineFuelConsumption());
            return StageFuelConsumption;
        }
        public double GetStageFuelTanksCapacity(List<FuelTanks> FuelTanks)
        {
            StageFuelTanksCapacity = FuelTanks.Sum(fuelTanks => fuelTanks.GetFuelTankCapacity());
            return StageFuelTanksCapacity;
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
                $"Stage weight is {StageWeight} kg." +
                $"Stage fuel tanks capacity is {StageFuelTanksCapacity} kg" +
                $"Stage fuel consumption is {StageFuelConsumption} kg/sec";
        }
    }
}
