﻿using System.Collections.Generic;
using Rocket.AdditionalStructure;
using Rocket.ComfortStructure;
using Rocket.Mechanics;
using Rocket.RocketFactory;

namespace Rocket
{
    public class Rocket:IRocket
    {
        private RocketStage _mechanics;
        private List<IComfortStructure> _comforts;
        private List<IAdditionalStructure> _additions;
        
        private readonly int _maxnumberofseats = 15;

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
            if (NumberOfAstronauts > _maxnumberofseats)
            {
                return false;
                // throw new Exception("Not enough space for astronauts!");
            }
            return true;
        }
        public string GetInfo()
        {
            return $"Rocket has {Mass} tons and {NumberOfAstronauts} astronauts." +
                   $"Ready to launch: {IsReadyToLaunch()}.";
        }
    }
}