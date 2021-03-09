using System;
using Rocket.AdditionStructure;

namespace Rocket.RocketFactory
{
    public class AdditionFactory : Factory
    {
        public override IAdditionStructure Create(int n)
        {
            return n switch
            {
                1 => new RestRoom(12, 3),
                2 => new RescueCapsule(10, 2),
                3 => new ObservationDeck(15, 5),
                _ => throw new ArgumentOutOfRangeException(nameof(n), n, "Wrong range choose 1-3")
            };
        }
    }
}