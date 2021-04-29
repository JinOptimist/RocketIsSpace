using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return RedirectToAction("Creation");
        }

        public IActionResult Remove(long id)
        {
            _bankAccountRepository.Remove(id);

            var user = _userService.GetCurrent();

            var newId = user.BankAccounts?.FirstOrDefault()?.Id;
            if (newId != null)
            {
                //return RedirectToAction("Index",  new { id = (long)newId });
                return Redirect($"/Account/Index?id={newId}");
            }
            return RedirectToAction("Creation");
        }

        [HttpGet]
        public IActionResult Creation()
        {
            return View("~/Views/Bank/Account/Creation.cshtml");
        }

        [HttpPost]
        public IActionResult Creation(BankAccountViewModel viewModel)
        {
            int accountLifeTime;
            if (viewModel.Currency == Currency.BYN) //заменить двойной if
            {
                if (viewModel.Type == null) 
                { 
                    viewModel.Type = "Счет"; 
                }
                accountLifeTime = 5;
            }
            else
            {
                if (viewModel.Type == null) 
                {
                    viewModel.Type = "Валютный счет";
                }
                accountLifeTime = 3;
            }

            StringBuilder sb = new StringBuilder();

            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                sb.Append(rnd.Next(0, 9));
            }
            viewModel.AccountNumber = sb.ToString();

            viewModel.CreationDate = DateTime.Now;

            viewModel.ExpireDate = viewModel.CreationDate.AddYears(accountLifeTime);

            var modelDB =
                _mapper.Map<BankAccount>(viewModel);

            var user = _userService.GetCurrent();

            modelDB.Owner = user;
            
            _bankAccountRepository.Save(modelDB);

            var id = user.BankAccounts?.
                SingleOrDefault(x => x.AccountNumber == viewModel.AccountNumber)
                .Id;

            return RedirectToAction("Index", new { id });
        }
    }
}
