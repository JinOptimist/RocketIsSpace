using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.RocketModels
{
    public class ComfortFormViewModel
    {
        public long Id { get; set; }

        public int ToiletCount { get; set; }

        public int KitchenSeatsCount { get; set; }

        public int StorageCapacity { get; set; }

        public int SleepingCapsulesCount { get; set; }
    }
}
