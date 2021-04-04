using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class Comfort : BaseModel
    {
        public int ToiletCount { get; set; }

        public int KitchenSeatsCount { get; set; }

        public int StorageCapacity { get; set; }

        public int SleepingCapsulesCount { get; set; }
    }
}
