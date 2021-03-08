using System;
using Rocket.AdditionalStructure;

namespace Rocket.RocketFactory
{
    public class AdditionsFactory:Factory
    {
        public override IAdditionalStructure Create(int n)
        {
            return n switch
            {
                1 => new RestRoom(),
                2 => new RescueCapsule(),
                3 => new ObservationDeck(),
                _ => throw new ArgumentOutOfRangeException(nameof(n), n, "Wrong range choose 1-3")
            };
        }
    }
}