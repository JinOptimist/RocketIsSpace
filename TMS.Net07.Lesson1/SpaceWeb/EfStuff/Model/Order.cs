using System;
using System.Collections.Generic;


namespace SpaceWeb.EfStuff.Model
{
    public class Order:BaseModel
    {
        public string  Name { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDateTime { get; set; }

        public virtual Client Client { get; set; }
        public virtual List<OrdersEmployes> OrdersEmployes { get; set; }

        public virtual List<AdditionStructure> AdditionsList { get; set; }
        public virtual List<ComfortStructure> ComfortsList { get; set; }

        public virtual List<Rocket> Rockets { get; set; }
        

    }
}