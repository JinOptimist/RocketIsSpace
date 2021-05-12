using System;

namespace SpaceWeb.Models.Human
{
    public class HumanOrderViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDateTime { get; set; }
    }
}