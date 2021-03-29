using System.Collections.Generic;
using Rocket2.AdditionStructure;
using Rocket2.ComfortStructure;
using Rocket2.Mechanics;

namespace Rocket2.RocketFactory
{
    public class RocketFactory
    {
        public static Rocket Create()
        {
            var mechanicFactory = new MechanicFactory();
            var comfortFactory = new ComfortFactory();
            var additionFactory = new AdditionFactory();

            var comfortsList = new List<IComfortStructure>();
            comfortsList.Add((IComfortStructure)comfortFactory.Create(1));
            comfortsList.Add((IComfortStructure)comfortFactory.Create(2));
            comfortsList.Add((IComfortStructure)comfortFactory.Create(3));

            var additionsList = new List<IAdditionStructure>();
            additionsList.Add((IAdditionStructure)additionFactory.Create(1));
            additionsList.Add((IAdditionStructure)additionFactory.Create(2));
            additionsList.Add((IAdditionStructure)additionFactory.Create(3));

            return new Rocket("TMS1", 10, (RocketStage)mechanicFactory.Create(2), comfortsList, additionsList);
        }
    }
}