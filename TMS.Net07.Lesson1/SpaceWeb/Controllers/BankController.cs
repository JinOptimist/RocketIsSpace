using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
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
        private ProfileRepository _profileRepository;

        public BankController(ProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
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
            var userprofile = new Profile()
            {
                Name = model.Name,
                Surname = model.Surname,
                BirthDate = model.BirthDate,
                Sex = model.Sex,
                PhoneNumber = model.PhoneNumber,
                PostAddress = model.PostAddress,
                IdentificationPassport = model.IdentificationPassport

            };

            _profileRepository.Save(userprofile);
            
            return RedirectToAction("UserProfileDataOutput");
        }
        
        public IActionResult UserProfileDataOutput()
        {
            var profileDateOutput = _profileRepository.GetAll()
                .Select(x => new UserProfileViewModel()
                {
                    Name = x.Name,
                    Sex = x.Sex,
                    BirthDate = x.BirthDate

                })
                .ToList();

            return View(profileDateOutput);
        }

        [HttpGet]
        public IActionResult Account ()
        {
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
            return View(modelNew);
        }
        
        [HttpPost]
        public IActionResult RemoveAccount(BankAccountViewModel model)
        {
            _bankAccountRepository.Remove(model.BankAccountId);

            return RedirectToAction("Account", "Bank");
        }
    }
}