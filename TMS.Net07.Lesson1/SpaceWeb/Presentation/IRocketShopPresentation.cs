using SpaceWeb.Models;
using SpaceWeb.Models.Human;
using SpaceWeb.Models.RocketModels;
using System.Collections.Generic;

namespace SpaceWeb.Presentation
{
    public interface IRocketShopPresentation
    {
        ComplexRocketShopViewModel GetCollectionRocketShopViewModel();
        void SaveRocket(ShopRocketViewModel model);
    }
}