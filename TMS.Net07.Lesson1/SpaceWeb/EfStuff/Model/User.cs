using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class User : BaseModel
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }
    }
}
