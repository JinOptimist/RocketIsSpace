using System.Collections.Generic;
using Rocket.AdditionStructure;
using Rocket.ComfortStructure;
using Rocket.Mechanics;
using Rocket.RocketFactory;
using System.Linq;

namespace Rocket
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

        public double Mass
        {
            get
            {
                return _comforts.Sum(mass => mass.Mass)
                       + _additions.Sum(mass => mass.Mass)
                       + _mechanics.GetStageMass();
            }
        }

        public int NumberOfAstronauts { get; }

        public bool IsReadyToLaunch()
        {
            return NumberOfAstronauts <= _numberofseats;
        }

        public string GetInfo()
        {
            return $"Rocket has {Mass} tons and {NumberOfAstronauts} astronauts." +
                   $"Ready to launch: {IsReadyToLaunch()}.";
        }
    }
}