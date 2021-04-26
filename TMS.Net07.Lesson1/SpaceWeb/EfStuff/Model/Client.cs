using System.Collections.Generic;

namespace SpaceWeb.EfStuff.Model
{
    public class Client : BaseModel
    {
        public long ForeignKeyProfile { get; set; }
        public virtual HumanProfile Profile { get; set; }

        public virtual List<Order> Orders { get; set; }

    }
}