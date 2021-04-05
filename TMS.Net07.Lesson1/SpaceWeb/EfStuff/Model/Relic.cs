using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class Relic : BaseModel
    {
        public string ImageUrl { get; set; }

        public string RelicName { get; set; }

        public int Price { get; set; }

        public int Count { get; set; }
    }
}
