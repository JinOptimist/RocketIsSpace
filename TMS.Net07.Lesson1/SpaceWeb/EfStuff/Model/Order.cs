using System;
using System.Collections.Generic;


namespace SpaceWeb.EfStuff.Model
{
    public class Order:BaseModel
    {
        public string  Name { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDateTime { get; set; }
        public virtual List<AdditionStructureDBmodel> AdditionsList { get; set; }
        public virtual List<ComfortStructureDBmodel> ComfortsList { get; set; }
        public virtual Client Client { get; set; }
        public virtual List<OrdersEmployes> OrdersEmployes { get; set; }
    }

}