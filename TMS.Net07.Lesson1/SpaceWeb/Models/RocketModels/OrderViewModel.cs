using System;
using System.Collections.Generic;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.Models.RocketModels
{
    public class OrderViewModel
    {
        public string  Name { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDateTime { get; set; }
        public List<AdditionStructureViewModel> AdditionsList { get; set; }
        public List<ComfortStructureViewModel> ComfortsList { get; set; }
    }
}