using System.Collections.Generic;
using System.Linq;
using Rocket2.AdditionStructure;
using Rocket2.ComfortStructure;
using Rocket2.Mechanics;

namespace Rocket2
{
    public class Rocket : IRocket
    {
        private RocketStage _mechanics;
        private List<IComfortStructure> _comforts;
        private List<IAdditionStructure> _additions;
        
        private readonly int _numberofseats = 15;

        public Rocket(string name, int numberofastronauts, RocketStage mechanics,
            List<IComfortStructure> comforts, List<IAdditionStructure> additions)
        {
            Name = name;
            NumberOfAstronauts = numberofastronauts;

            _mechanics = mechanics;
            _comforts = comforts;
            _additions = additions;
            
        }

        public string Name { get; }
        public int Mass { get; } = 0;
        public int NumberOfAstronauts { get; }

        public bool IsReadyToLaunch()
        {
            if (NumberOfAstronauts > _numberofseats)
            {
                return false;
            }

            return true;
        }

        public string GetInfo()
        {
            return $"Rocket has {Mass} tons and {NumberOfAstronauts} astronauts." +
                   $"Ready to launch: {IsReadyToLaunch()}.";
        }
        public double GetMass()
        {
            var rocketMass = _comforts.Sum(mass => mass.Mass)
                                  + _additions.Sum(mass => mass.Mass)
                                  + _mechanics.GetStageMass();
            return rocketMass;
        }
    }
}