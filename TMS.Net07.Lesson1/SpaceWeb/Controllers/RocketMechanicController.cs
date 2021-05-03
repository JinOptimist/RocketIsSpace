using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Presentation;
using System.Linq;

namespace SpaceWeb.Controllers
{
    public class RocketMechanicController : Controller
    {
        private IRocketStageRepository _rocketStageRepository;
        private IMapper _mapper;
        private IRocketMechanicPresentation _rocketMechanicPresentation;

        public RocketMechanicController(IRocketStageRepository rocketStageRepository, IMapper mapper, IRocketMechanicPresentation rocketMechanicPresentation)
        {
            _rocketStageRepository = rocketStageRepository;
            _mapper = mapper;
            _rocketMechanicPresentation = rocketMechanicPresentation;
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
            var models = _rocketMechanicPresentation.GetIndexViewModel();

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
            var viewModel = _rocketMechanicPresentation.GetRocketStageAddViewModel(id);
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
