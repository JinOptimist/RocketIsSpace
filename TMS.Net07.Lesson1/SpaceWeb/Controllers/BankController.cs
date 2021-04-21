using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;

using System;
using System.Text;
using System.Linq;

namespace SpaceWeb.Controllers
{
    public class BankController : Controller
    {
        private BankAccountRepository _bankAccountRepository;
        private ProfileRepository _profileRepository;
        private UserRepository _userRepository;

        public BankController(BankAccountRepository bankAccountRepository, 
            ProfileRepository profileRepository, 
            UserRepository userRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
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
                SurName = model.SurName,
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
        public IActionResult Account(BankAccountViewModel viewModel)
        {
            
            if ( viewModel.Currency == Currency.BYN)
            {
                viewModel.Type = "Счет";
            }
            else
            {
                viewModel.Type = "Валютный счет";
            }

            StringBuilder sb = new StringBuilder();

            Random rnd = new Random();
            
            for (int i = 0; i<10; i++)
            {
                sb.Append(rnd.Next(0, 9));
            }
            viewModel.BankAccountId = sb.ToString();

            var bankAccountDB = new BankAccount
            {
                Amount = viewModel.Amount,
                BankAccountId = viewModel.BankAccountId,
                Currency = viewModel.Currency,
                Type = viewModel.Type,
            };

            bankAccountDB.Owner = _userRepository.Get(viewModel.OwnerId);
            _bankAccountRepository.Save(bankAccountDB);

            //user.BankAccounts.Add(bankAccountDB);
            //_userRepository.Save(user);


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