using Microsoft.AspNetCore.Mvc;
using SpaceWeb.Models;

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SpaceWeb.EfStuff;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;

namespace SpaceWeb.Controllers
{
    public class BankController : Controller
    {
        private BankAccountRepository _bankAccountRepository;

        public BankController(BankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }
        public IActionResult Bank()
        {
            var input = new RegistrationViewModel();
           
            return View(input);
        }
        [HttpGet]
        public IActionResult Login()
        {
            var model = new ProfileViewModel();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Login(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Bio = model.UserName + model.Password;
            return View(model);
        }
        public IActionResult Home()
        {
            var model = new RocketPreviewViewModel()
            {
                Name = "Soyus",
                Url = "image/Client/start.jpg"
            };

            return View(model);
        }
        public IActionResult Contacts()
        {
            var input = new ContactsViewModel()
            {
                PhoneNumber = "+375291191293",
                Email = "alesya.lis.1@mail.ru",
                PostAddress = "Belarus, Minsk, Timeriazeva 67"
            };
            return View(input);
        }
        [HttpGet]
        public IActionResult UserProfile()
        {
            var user = new UserProfileViewModel();

            return View(user);
        }

        [HttpPost]
        public IActionResult UserProfile(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           
            return View(model);
        }

        [HttpGet]
        public IActionResult Account ()
        {
            //var model = new List<BankAccountViewModel>();
            //foreach (var accountDB in _dbContext.BankAccount)
            //{
            //    var accountVM = new BankAccountViewModel
            //    {
            //        Amount = accountDB.Amount,
            //        BankAccountId = accountDB.BankAccountId,
            //        Currency = accountDB.Currency,
            //        Type = accountDB.Type
            //    };
            //    model.Add(accountVM);
            //}

            var model = _bankAccountRepository
                .GetAll()
                .Select(dbModel => new BankAccountViewModel
                {
                    BankAccountId = dbModel.BankAccountId,
                    Amount = dbModel.Amount,
                    Currency = dbModel.Currency,
                    Type = dbModel.Type
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Account(BankAccountViewModel model)
        {
            if ( model.Currency == "BYN")
            {
                model.Type = "Счет";
            }
            else
            {
                model.Type = "Валютный счет";
            }

            StringBuilder sb = new StringBuilder();

            Random rnd = new Random();
            
            for (int i = 0; i<10; i++)
            {
                sb.Append(rnd.Next(0, 9));
            }
            model.BankAccountId = sb.ToString();

            var modelDB = new BankAccount
            {
                Amount = model.Amount,
                BankAccountId = model.BankAccountId,
                Currency = model.Currency,
                Type = model.Type
            };

            _bankAccountRepository.Save(modelDB);

            //var modelNew = new List<BankAccountViewModel>();

            var modelNew = _bankAccountRepository
                .GetAll()
                .Select(dbModel => new BankAccountViewModel
                {
                    BankAccountId = dbModel.BankAccountId,
                    Amount = dbModel.Amount,
                    Currency = dbModel.Currency,
                    Type = dbModel.Type
                })
                .ToList();

            //foreach (var accountDB in _dbContext.BankAccount)
            //{
            //    var accountVM = new BankAccountViewModel
            //    {
            //        Amount = accountDB.Amount,
            //        BankAccountId = accountDB.BankAccountId,
            //        Currency = accountDB.Currency,
            //        Type = accountDB.Type
            //    };
            //    modelNew.Add(accountVM);
            //}

            return View(modelNew);
        }
        [HttpPost]
        public IActionResult RemoveAccount(BankAccountViewModel model)
        {
            //var accountToRemove = _dbContext.BankAccount
            //    .SingleOrDefault(x => x.BankAccountId == model.BankAccountId);

            //_dbContext.BankAccount.Remove(accountToRemove);
            //_dbContext.SaveChanges();

            _bankAccountRepository.Remove(model.BankAccountId);

            return RedirectToAction("Account", "Bank");
        }
    }
}