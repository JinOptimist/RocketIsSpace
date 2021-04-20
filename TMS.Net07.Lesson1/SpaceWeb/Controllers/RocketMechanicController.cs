using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SpaceWeb.Controllers
{
    public class RocketMechanicController : Controller
    {
        private RocketStageRepository _rocketStageRepository;
        private IMapper _mapper;

        public RocketMechanicController(RocketStageRepository rocketStageRepository, IMapper mapper)
        {
            _rocketStageRepository = rocketStageRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Main()
        {
            var model = new MainViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Main(MainViewModel mainViewModel)
        {
            return View(mainViewModel);
        }
        [Authorize]
        public IActionResult RocketStageIndex()
        {
            var models = _rocketStageRepository
                .GetAll()
                .Select(dbModel => _mapper.Map<RocketStageAddViewModel>(dbModel))
                .ToList();

            return View(models);
        }
        [Authorize]
        public IActionResult RocketStageRemove(long id) 
        {
            _rocketStageRepository.Remove(id);
            return RedirectToAction("RocketStageIndex");
        }
        [Authorize]
        [HttpGet]
        public IActionResult RocketStageAdd(long id = 0)
        {
            var rocketStage = _rocketStageRepository.Get(id);
            var viewModel = _mapper.Map<RocketStageAddViewModel>(rocketStage)
                ?? new RocketStageAddViewModel();
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        public IActionResult RocketStageAdd(RocketStageAddViewModel rocketStageAddViewModel)
        {
            if (!ModelState.IsValid) 
            {
                return View(rocketStageAddViewModel);
            }

            var rocketStage = _mapper.Map<RocketStage>(rocketStageAddViewModel);
            _rocketStageRepository.Save(rocketStage);
            return RedirectToAction("RocketStageIndex");
        }
    }
}
