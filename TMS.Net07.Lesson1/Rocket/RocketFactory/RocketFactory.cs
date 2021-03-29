using System.Collections.Generic;
using Rocket.AdditionStructure;
using Rocket.ComfortStructure;

namespace Rocket.RocketFactory
{
    public class RocketFactory
    {
        public static Rocket Create()
        {
            var mechanicFactory = new MechanicFactory();
            var comfortFactory = new ComfortFactory();
            var additionFactory = new AdditionFactory();

            var comfortsList = new List<IComfortStructure>();
            comfortsList.Add(comfortFactory.Create(1));
            comfortsList.Add(comfortFactory.Create(2));
            comfortsList.Add(comfortFactory.Create(3));

            var additionsList = new List<IAdditionStructure>();
            additionsList.Add(additionFactory.Create(1));
            additionsList.Add(additionFactory.Create(2));
            additionsList.Add(additionFactory.Create(3));

            return new Rocket("TMS1", 10, mechanicFactory.Create(2), comfortsList, additionsList);
        }
    }
}