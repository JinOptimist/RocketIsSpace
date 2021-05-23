using System.Collections.Generic;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.Models.RocketModels
{
    public class ComplexRocketShopViewModel
    {
        public List<ShopRocketViewModel> AddRockets { get; set; }
        public List<long> RocketIds { get; set; }

        public long ClientId { get; set; }
    }
}