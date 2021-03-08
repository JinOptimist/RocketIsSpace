using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.RocketFactory;

namespace Rocket.Mechanics
{
    public class RocketStage:IRocket
    {
        private double _stageWeight;
        private double _fuelConsumption;
        private double _fuelTanksCapacity;
        public RocketStage(List<Engines> engines, List<FuelTank> fuelTank)
        {
            Engines = engines;
            FuelTanks = fuelTank;
            _stageWeight = GetStageWeight();
            _fuelConsumption = GetStageFuelConsumption();
            _fuelTanksCapacity = GetStageFuelTanksCapacity();
        }
        public List<Engines> Engines { get;}
        public List<FuelTank> FuelTanks { get;}
        
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
                $"{Environment.NewLine}Stage weight is {_stageWeight} kg." +
                $"{Environment.NewLine}Stage fuel tanks capacity is {_fuelTanksCapacity} kg" +
                $"{Environment.NewLine}Stage fuel consumption is {_fuelConsumption} kg/sec";
        }
    }
}
