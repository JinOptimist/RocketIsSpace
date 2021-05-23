using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace SpaceWeb.EfStuff.Model
{
    public class Order:BaseModel
    {
        public string  Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public DateTime OrderDateTime { get; set; }
        public OrderStates State { get; set; }
        public virtual Client Client { get; set; }
        public virtual List<OrdersEmployes> OrdersEmployes { get; set; }

        public virtual List<AdditionStructure> AdditionsList { get; set; }
        public virtual List<ComfortStructure> ComfortsList { get; set; }

        public virtual List<Rocket> Rockets { get; set; }
        

    }
}