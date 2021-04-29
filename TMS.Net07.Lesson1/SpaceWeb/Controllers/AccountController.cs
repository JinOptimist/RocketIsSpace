using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private BankAccountRepository _bankAccountRepository;
        private IMapper _mapper;
        private UserService _userService;

        public AccountController(BankAccountRepository bankAccountRepository,
            ProfileRepository profileRepository,
            IMapper mapper, UserService userService)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Index(long id)
        {
            if (id > 0)
            {
                var user = _userService.GetCurrent();
                var dbModel = user.BankAccounts.SingleOrDefault(x => x.Id == id);
                var viewModel = _mapper.Map<BankAccountViewModel>(dbModel);
                return View("~/Views/Bank/Account/Index.cshtml", viewModel);
            }
            return View();
        }

        public IActionResult Remove(long id)
        {
            _bankAccountRepository.Remove(id);

            return RedirectToAction("Cabinet", "Bank");
        }

        [HttpGet]
        public IActionResult Creation()
        {
            return View("~/Views/Bank/Account/Creation.cshtml");
        }
    }
}
