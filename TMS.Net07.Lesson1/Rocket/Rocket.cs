using System.Collections.Generic;
using Rocket.AdditionalStructure;
using Rocket.ComfortStructure;
using Rocket.Mechanics;
using Rocket.RocketFactory;
using System.Linq;

namespace Rocket
{
    public class Rocket:IRocket
    {
        private RocketStage _mechanics;
        private List<IComfortStructure> _comforts;
        private List<IAdditionalStructure> _additions;
        
        private readonly int _maxNumberOfSeats = 15;

        public Rocket(string name,int numberofastronauts,RocketStage mechanics,
            List<IComfortStructure> comforts,List<IAdditionalStructure> additions)
        {
            Name = name;
            NumberOfAstronauts = numberofastronauts;

            _mechanics = mechanics;
            _comforts = comforts;
            _additions = additions;
            
        }
        public string Name { get;}
        public int Mass { get; } = 0;
        public int NumberOfAstronauts { get;}
        
        public bool IsReadyToLaunch()
        {
            if (NumberOfAstronauts > _maxNumberOfSeats)
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
            var rocketWeight = _comforts.Sum(mass => mass.Weight)
                                  + _additions.Sum(mass => mass.Weight)
                                  + _mechanics.GetStageWeight();
            return rocketWeight;
        }
    }
}