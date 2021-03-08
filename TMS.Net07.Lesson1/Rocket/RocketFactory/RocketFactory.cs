using System.Collections.Generic;
using Rocket.AdditionalStructure;
using Rocket.ComfortStructure;

namespace Rocket.RocketFactory
{
    public class RocketFactory
    {
        public static Rocket Create()
        {
            var mechnicsFactory = new MechanicsFactory();
            var comfortFactory = new ComfortFactory();
            var addFactory = new AdditionsFactory();
            
            var comfList = new List<IComfortStructure>();
            comfList.Add(comfortFactory.Create(1));
            comfList.Add(comfortFactory.Create(2));
            comfList.Add(comfortFactory.Create(3));

            var addList = new List<IAdditionalStructure>();
            addList.Add(addFactory.Create(1));
            addList.Add(addFactory.Create(2));
            addList.Add(addFactory.Create(3));
            
            return new Rocket("TMS1",10,mechnicsFactory.Create(2),comfList,addList);
        }
    }
}