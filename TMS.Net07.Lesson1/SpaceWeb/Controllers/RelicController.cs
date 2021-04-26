using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    public class RelicController : Controller
    {
        private IRelicRepository _relicRepository;
        private AdvImageRepository _advImageRepository;
        private IMapper _mapper;
        private IRelicPresentation _relicPresentation;

        public RelicController(IRelicRepository relicRepository,
            AdvImageRepository advImageRepository, IMapper mapper,
            IRelicPresentation relicPresentation)
        {
            _relicRepository = relicRepository;
            _advImageRepository = advImageRepository;
            _mapper = mapper;
            _relicPresentation = relicPresentation;
        }

        public IActionResult Index()
        {
            var models = _relicPresentation.GetIndexViewModels();
            return View(models);
        }

        [HttpGet]
        public IActionResult Add(long id = 0)
        {
            var relic = _relicRepository.Get(id);
            var viewModel = _mapper.Map<RelicViewModel>(relic)
                ?? new RelicViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(RelicViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var relic = _mapper.Map<Relic>(viewModel);
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
