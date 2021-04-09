using System;
using System.Collections.Generic;
using Rocket2.AdditionStructure;
using Rocket2.ComfortStructure;

namespace SpaceWeb.EfStuff.Model
{
    public class Order:BaseModel
    {
        public string  Name { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDateTime { get; set; }
        public virtual List<AdditionStructureDBmodel> AdditionsList { get; set; }
        public virtual List<ComfortStructureDBmodel> ComfortsList { get; set; }
        
    }

}