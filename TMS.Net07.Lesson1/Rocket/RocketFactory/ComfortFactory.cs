using System;
using Rocket.ComfortStructure;

namespace Rocket.RocketFactory
{
    public class ComfortFactory : Factory
    {
        public override IComfortStructure Create(int n)
        {
            return n switch
            {
                1 => new Kitchen(20, 20, 10),
                2 => new Toilet(5),
                3 => new CommandCenter(100),
                _ => throw new ArgumentOutOfRangeException(nameof(n), n, "Choose between 1-3")
            };
        }
    }
}