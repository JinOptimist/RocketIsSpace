using System.Collections.Generic;

namespace SpaceWeb.EfStuff.Model
{
    public class Order : BaseModel
    {
        public virtual Client Client { get; set; }

        public virtual List<OrderList> OrderList { get; set; }

    }
}