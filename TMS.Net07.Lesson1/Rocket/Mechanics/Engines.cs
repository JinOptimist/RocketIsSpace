using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rocket.Mechanics
{
    public class Engines
    {
        public double engineWeight { get; private set; }
        public double engineLiftCapacity { get; private set; }
        public double engineFuelConsumption { get; private set; }
        public bool startEngine { get; set; }

        public Engines(double EngineWeight, double EngineLiftCapacity, double EngineFuelConsumption, bool StartEngine)
        {
            EngineWeight = engineWeight;
            EngineLiftCapacity = engineLiftCapacity;
            EngineFuelConsumption = engineFuelConsumption;
            StartEngine = startEngine;
        }

        public bool EngineStarted(bool startEngine)
        {
            if (startEngine == true)
            {
                return true;
            }
            return false;
        }

        public double GetEngineWeight()
        {
            return engineWeight;
        }

        public double GetEngineLiftCapacity()
        {
            return engineLiftCapacity;
        }

        public double GetEngineFuelConsumption()
        {
            return engineFuelConsumption;
        }
        public string GetInfo()
        {
            return $"Engine weight = {this.engineWeight} kg." +
                $"Engine lift capacity = {this.engineLiftCapacity} kg." +
                $"Engine fuel consumption = {this.engineFuelConsumption} kg/sec.";
        }
    }
}

