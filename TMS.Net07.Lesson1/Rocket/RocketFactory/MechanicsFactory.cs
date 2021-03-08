﻿using System.Collections.Generic;
using Rocket.ComfortStructure;
using Rocket.Mechanics;

namespace Rocket.RocketFactory
{
    public class MechanicsFactory:Factory
    {
        public override RocketStage Create(int n)
        {
            var enginesList = EnginesFactory.Create(3);
            var fuelTanksList = FuelTanksFactory.Create(10);
            return new RocketStage(enginesList, fuelTanksList);
        }
    }

    public class EnginesFactory
    {
        public static List<Engines> Create(int n)
        {
            var list = new List<Engines>();
            for (var i = 0; i < n; i++)
            {
                list.Add(new Engines(40,1500,900));
            }

            return list;
        }
    }

    public class FuelTanksFactory
    {
        public static List<FuelTank> Create(int n)
        {
            var list = new List<FuelTank>();
            for (var i = 0; i < n; i++)
            {
                list.Add(new FuelTank(40,1500));
            }

            return list;
        }
    }
}