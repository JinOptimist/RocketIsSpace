using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class Rocket : BaseModel
    {
        public int Cost { get; set; }

        public string Url { get; set; }
    }
}
