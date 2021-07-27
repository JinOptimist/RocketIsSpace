using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models.Bank;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    public class InsuranceController : Controller
    {
        private InsuranceTypeRepository _insuranceTypeRepository;
        private InsuranceRepository _insuranceRepository;
        private IMapper _mapper;
        private IUserService _userService;

        public InsuranceController(InsuranceTypeRepository insuranceTypeRepository, InsuranceRepository insuranceRepository,
            IMapper mapper, IUserService userService)
        {
            _insuranceTypeRepository = insuranceTypeRepository;
            _insuranceRepository = insuranceRepository;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Insurance()
        {
            var user = _userService.GetCurrent();

            var models = _insuranceRepository
                .GetAll()
                .Where(x => x.Owner.Id == user.Id)
                .Select(insurance =>
                    _mapper.Map<InsurancePrintViewModel>(insurance)
                )
                .ToList();
            return View(models);
        }

        [HttpGet]
        public ActionResult AddInsurance()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddInsurance(InsuranceViewModel model)
        {
            var selectedInsuranceType = _insuranceTypeRepository.GetPolis(model.InsuranceNameType, model.InsurancePeriod);
            var user = _userService.GetCurrent();

            var newInsurance = new Insurance()
            {
                InsuranceType = selectedInsuranceType,
                DateCreationing = DateTime.Now,
                Owner = user
            };
            _insuranceRepository.Save(newInsurance);

            return View();
    }
}
}
