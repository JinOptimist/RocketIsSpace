using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rocket.Mechanics
{
    public class Engine
    {
        public double EngineWeight { get; private set; }
        public double EngineLiftCapacity { get; private set; }
        public double EngineFuelConsumption { get; private set; }
        public Engine(double engineWeight, double engineLiftCapacity, double engineFuelConsumption)
        {
            EngineWeight = engineWeight;
            EngineLiftCapacity = engineLiftCapacity;
            EngineFuelConsumption = engineFuelConsumption;
        }
        public bool EngineStarted(bool startEngine)
        {
            if (startEngine == true)
            {
                return true;
            }
            return false;
        }
        public string GetInfo()
        {
            return $"Engine weight = {EngineWeight} kg." +
                $"{Environment.NewLine}Engine lift capacity = {EngineLiftCapacity} kg." +
                $"{Environment.NewLine}Engine fuel consumption = {EngineFuelConsumption} kg/sec.";
        }
    }
}

