using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Rocket
{
    public class Rocket
    {
        private List<IMechanics> _mechanics = new List<IMechanics>();
        private List<IComfort> _comforts = new List<IComfort>();
        private List<IAdditional> _additionals= new List<IAdditional>();
        
        private readonly int _maxnumberofseats = 15;
        private readonly int _maxmass = 308;
        
        public Rocket(string name, int mass, int numberofastronauts)
        {
            Name = name;
            Mass = mass;
            Numberofastronauts = numberofastronauts;
            
            Mechanics = _mechanics;
            Comforts = _comforts;
            Additionals = _additionals;
        }
        public string Name { get;}
        public int Mass { get;}
        public int Numberofastronauts { get;}

        public List<IMechanics> Mechanics
        {
            get => _mechanics;
            set => _mechanics = value;
        }
        public List<IComfort> Comforts
        {
            get => _comforts;
            set => _comforts = value;
        }
        public List<IAdditional> Additionals
        {
            get => _additionals;
            set => _additionals= value;
        }
        
        public bool IsReadyToLaunch()
        {
            if (Numberofastronauts > _maxnumberofseats)
            {
                return false;
                // throw new Exception("Not enough space for astronauts!");
            }
            if (Mass > _maxmass)
            {
                return false;
                //throw new Exception("You very heavy");
            }
            return true;
        }
        public string GetInfo()
        {
            return $"Rocket has {Mass} tons and {Numberofastronauts} astronauts." +
                   $"Ready to launch: {IsReadyToLaunch()}.";
        }
    }
}