using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    public class RelicController : Controller
    {
        private RelicRepository _relicRepository;
        private AdvImageRepository _advImageRepository;
        private IMapper _mapper;

        public RelicController(RelicRepository relicRepository,
            AdvImageRepository advImageRepository, IMapper mapper)
        {
            _relicRepository = relicRepository;
            _advImageRepository = advImageRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var models = _relicRepository
                .GetAll()
                .Select(dbModel => 
                    _mapper.Map<RelicViewModel>(dbModel)
                )
                .ToList();
            return View(models);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new RelicViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(RelicViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var relic = new Relic()
            {
                RelicName = viewModel.RelicName,
                ImageUrl = viewModel.ImageUrl,
                Price = viewModel.Price,
                Count = viewModel.Count
            };

            _relicRepository.Save(relic);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(long id)
        {
            _relicRepository.Remove(id);
            return RedirectToAction("Index");
        }

        public IActionResult GetAdvImage()
        {
            var viewModel = _advImageRepository
                .GetAll()
                .Select(dbModel => 
                    _mapper.Map<AdvImageViewModel>(dbModel)
                )
                .ToList();
            return View();
        }
    }
}
