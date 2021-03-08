using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rocket.Mechanics
{
    public class Engine
    {
        public double EngineMass { get; private set; }
        public double EngineLiftCapacity { get; private set; }
        public double EngineFuelConsumption { get; private set; }

        public Engine(double engineMass, double engineLiftCapacity, double engineFuelConsumption)
        {
            EngineMass = engineMass;
            EngineLiftCapacity = engineLiftCapacity;
            EngineFuelConsumption = engineFuelConsumption;
        }

        public bool EngineStarted(bool startEngine)
        {
            return startEngine;
        }

        public string GetInfo()
        {
            return $"Engine mass = {EngineMass} kg." +
                   $"{Environment.NewLine}Engine lift capacity = {EngineLiftCapacity} kg." +
                   $"{Environment.NewLine}Engine fuel consumption = {EngineFuelConsumption} kg/sec.";
        }
    }
}