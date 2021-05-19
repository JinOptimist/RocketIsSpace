using SpaceWeb.Models;
using SpaceWeb.Models.Human;
using SpaceWeb.Models.RocketModels;
using System.Collections.Generic;

namespace SpaceWeb.Presentation
{
    public interface IRocketShopPresentation
    {
        CollectionRocketShopViewModel GetCollectionRocketShopViewModel();
        void SaveOrder(CollectionRocketShopViewModel collection);
        void SaveRocket(AddShopRocketViewModel model);
    }
}