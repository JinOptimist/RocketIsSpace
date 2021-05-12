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
        private UserService _userService;

        public InsuranceController(InsuranceTypeRepository insuranceTypeRepository, InsuranceRepository insuranceRepository,
            UserService userService)
        {
            _insuranceTypeRepository = insuranceTypeRepository;
            _insuranceRepository = insuranceRepository;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Insurance()
        {
            return View();
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
