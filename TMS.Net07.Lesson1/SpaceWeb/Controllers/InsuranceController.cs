using Microsoft.AspNetCore.Mvc;
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
        private InsuranceRepository _insuranceTypeRepository;
        private UserService _userService;

        public InsuranceController(InsuranceRepository insuranceTypeRepository,
            UserService userService)
        {
            _insuranceTypeRepository = insuranceTypeRepository;
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
            return View();
        }
    }
}
