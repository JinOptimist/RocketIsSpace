using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models.RocketModels;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System.IO;

namespace SpaceWeb.Controllers
{
    public class ComfortController : Controller
    {
        private IMapper _mapper;
        private IComfortRepository _comfortRepository;
        public ComfortController(IMapper mapper, IComfortRepository comfortRepository)
        {
            _mapper = mapper;
            _comfortRepository = comfortRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult ComfortPage()
        {
            var model = new ComfortFormViewModel();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult ComfortPage(ComfortFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var comfort = _mapper.Map<Comfort>(viewModel);

            _comfortRepository.Save(comfort);
            return RedirectToAction("ComfortPage", "Comfort");
        }

        [Authorize]
        public IActionResult ToiletPage()
        {
            return View();
        }
        [Authorize]
        public IActionResult KitchenPage()
        {
            return View();
        }
        [Authorize]
        public IActionResult CCenterPage()
        {
            return View();
        }
        [Authorize]
        public IActionResult CapsulePage()
        {
            return View();
        }
    }
}
