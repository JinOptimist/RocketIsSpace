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

        public virtual User Author { get; set; }

        public virtual User Qa { get; set; }

        public virtual List<User> UserWhoFavouriteTheRocket { get; set; }
    }
}
