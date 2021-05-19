using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.Controllers.CustomAttribute;
using SpaceWeb.Models.RocketModels;
using SpaceWeb.Presentation;

namespace SpaceWeb.Controllers
{
    public class RocketShopController : Controller
    {

        private readonly IRocketShopPresentation _rocketShopPresentation;

        public RocketShopController(IRocketShopPresentation rocketShopPresentation)
        {
            _rocketShopPresentation = rocketShopPresentation;
        }

        [HttpGet]
        [Authorize]
        public IActionResult RocketShop()
        {
            var collection = _rocketShopPresentation.GetCollectionRocketShopViewModel();
            return View(collection);
        }

        [HttpPost]
        [Authorize]
        public IActionResult RocketShop(CollectionRocketShopViewModel collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            _rocketShopPresentation.SaveOrder(collection);

            return View(collection);
        }

        [HttpGet]
        [IsAdmin]
        public IActionResult AdminAddRocket()
        {
            var model = new AddShopRocketViewModel();
            return View(model);
        }

        [HttpPost]
        [IsAdmin]
        public IActionResult AdminAddRocket(AddShopRocketViewModel model)
        {
            _rocketShopPresentation.SaveRocket(model);
            return View();
        }
    }
}