using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using System.Linq;

namespace SpaceWeb.Controllers
{
    public class RocketMechanicController : Controller
    {
        private RocketStageRepository _rocketStageRepository;
        public RocketMechanicController(RocketStageRepository rocketStageRepository)
        {
            _rocketStageRepository = rocketStageRepository;
        }
        [HttpGet]
        public IActionResult Main()
        {
            var model = new MainViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Main(MainViewModel mainViewModel)
        {
            return View(mainViewModel);
        }

        public IActionResult RocketStageIndex()
        {
            var models = _rocketStageRepository
                .GetAll()
                .Select(x => new RocketStageAddViewModel()
                {
                    Id = x.Id,
                    RocketStageModel = x.RocketStageModel,
                    ImageUrl = x.ImageUrl,
                    Weight = x.Weight,
                    EnginesModel = x.EnginesModel,
                    FuelTanksModel = x.FuelTanksModel,
                    RocketStageDescription = x.RocketStageDescription
                })
                .ToList();

            return View(models);
        }

        public IActionResult RocketStageRemove(long id) 
        {
            _rocketStageRepository.Remove(id);
            return RedirectToAction("RocketStageIndex");
        }

        [HttpGet]
        public IActionResult RocketStageAdd()
        {
            var rocketStageAddViewModel = new RocketStageAddViewModel();
            return View(rocketStageAddViewModel);
        }

        [HttpPost]
        public IActionResult RocketStageAdd(RocketStageAddViewModel rocketStageAddViewModel)
        {
            if (!ModelState.IsValid) 
            {
                return View(rocketStageAddViewModel);
            }
            var rocketStage = new RocketStage()
            {
                RocketStageModel = rocketStageAddViewModel.RocketStageModel,
                ImageUrl = rocketStageAddViewModel.ImageUrl,
                Weight = rocketStageAddViewModel.Weight,
                EnginesModel = rocketStageAddViewModel.EnginesModel,
                FuelTanksModel = rocketStageAddViewModel.FuelTanksModel,
                RocketStageDescription = rocketStageAddViewModel.RocketStageDescription
            };
            _rocketStageRepository.Save(rocketStage);
            return View(rocketStageAddViewModel);
        }
    }
}
