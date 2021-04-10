using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models.RocketModels;

namespace SpaceWeb.Controllers
{
    public class ComfortController : Controller
    {
        private ComfortRepository _comfortRepository;
        public ComfortController(ComfortRepository comfortRepository)
        {
            _comfortRepository = comfortRepository;
        }

        [HttpGet]
        public IActionResult ComfortPage()
        {
            var model = new ComfortFormViewModel();
            return View(model);
            //return View("Rocket/Comfort/ComfortPage", model);
        }

        [HttpPost]
        public IActionResult ComfortPage(ComfortFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Comfort/ComfortPage",viewModel);
            }

            var comfort = new Comfort()
            {
                ToiletCount = viewModel.ToiletCount,
                KitchenSeatsCount = viewModel.KitchenSeatsCount,
                StorageCapacity = viewModel.StorageCapacity,
                SleepingCapsulesCount = viewModel.SleepingCapsulesCount
            };

            _comfortRepository.Save(comfort);
            return RedirectToAction("ComfortPage");
        }
        
        public IActionResult ToiletPage()
        {
            return View("Comfort/ToiletPage");
        }

        public IActionResult KitchenPage()
        {
            return View("Comfort/KitchenPage");
        }

        public IActionResult CCenterPage()
        {
            return View("Comfort/CCenterPage");
        }

        public IActionResult CapsulePage()
        {
            return View("Comfort/CapsulePage");
        }
    }
}
