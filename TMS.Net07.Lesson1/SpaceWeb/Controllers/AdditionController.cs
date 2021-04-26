using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models.RocketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    public class AdditionController : Controller
    {
        private AdditionRepository _additionRepository;
        private IMapper _mapper;

        public AdditionController(AdditionRepository additionRepository, IMapper mapper)
        {
            _additionRepository = additionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult AdditionPage()
        {
            var model = new AdditionFormViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AdditionPage(AdditionFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //make mapper here
            var addition = new Addition()
            {
                RescueCapsuleCount = model.RescueCapsuleCount,
                RestRoomCount = model.RestRoomCount,
                Id = model.Id,
                BotanicalCenterCount = model.BotanicalCenterCount,
                ObservarionDeckCount = model.ObservarionDeckCount
            };

            _additionRepository.Save(addition);
            return View();
        }
    }
}
