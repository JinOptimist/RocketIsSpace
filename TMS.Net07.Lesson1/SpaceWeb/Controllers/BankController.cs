﻿using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;

using System;
using System.Text;
using System.Linq;
using AutoMapper;
using Profile = SpaceWeb.EfStuff.Model.Profile;

namespace SpaceWeb.Controllers
{
    public class BankController : Controller
    {
        private BankAccountRepository _bankAccountRepository;
        private ProfileRepository _profileRepository;
        private IMapper _mapper;
        private UserRepository _userRepository;


        public BankController(BankAccountRepository bankAccountRepository, 
            ProfileRepository profileRepository, 
            UserRepository userRepository,
            IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _mapper = mapper;
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
            var user = _userRepository.Get(model.OwnerId);
            var userprofile = new Profile()
            {
                Name = model.Name,
                SurName = model.SurName,
                BirthDate = model.BirthDate,
                Sex = model.Sex,
                PhoneNumber = model.PhoneNumber,
                PostAddress = model.PostAddress,
                IdentificationPassport = model.IdentificationPassport,
                Owner = user

            };

            _profileRepository.Save(userprofile);
            
            return RedirectToAction("UserProfileDataOutput");
        }
        
        public IActionResult UserProfileDataOutput()
        {
            var profileDateOutput = _profileRepository
                .GetAll()
                .Select(dbModel =>_mapper.Map<UserProfileViewModel>(dbModel)
                )
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

            var user = _userRepository.Get(model.OwnerId);
            var modelDB = new BankAccount
            {
                Amount = model.Amount,
                BankAccountId = model.BankAccountId,
                Currency = model.Currency,
                Type = model.Type,
                Owner = user
            };

            //user.BankAccounts.Add(modelDB);
            //_userRepository.Save(user);
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