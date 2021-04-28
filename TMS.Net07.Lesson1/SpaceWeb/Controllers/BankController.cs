﻿using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SpaceWeb.EfStuff;
using AutoMapper;
using Profile = SpaceWeb.EfStuff.Model.Profile;
using SpaceWeb.Service;
using Microsoft.AspNetCore.Authorization;
using SpaceWeb.Controllers.CustomAttribute;
using SpaceWeb.EfStuff.Repositories.IRepository;

namespace SpaceWeb.Controllers
{
    public class BankController : Controller
    {
        private BankAccountRepository _bankAccountRepository;
        private ProfileRepository _profileRepository;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private UserService _userService;

        public BankController(BankAccountRepository bankAccountRepository,
            ProfileRepository profileRepository,
            IUserRepository userRepository,
            IMapper mapper, UserService userService)
        {
            _bankAccountRepository = bankAccountRepository;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
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
        public IActionResult UserProfile(long id = 0)
        {
            var userprofile = _profileRepository.Get(id);
            var profile = _mapper.Map<UserProfileViewModel>(userprofile)
                ?? new UserProfileViewModel();
            return View(profile);
        }

        [HttpPost]
        public IActionResult UserProfile(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //var user = _userRepository.Get(model.Id);
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

            var user = _userService.GetCurrent();
            userprofile.User = user;
            //userprofile.UserRef = user.Id;
               
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

        [Authorize]
        [IsBankClientOrHigher]
        [HttpGet]
        public IActionResult Cabinet()
        {
            var user = _userService.GetCurrent();
            var modelNew = user.BankAccounts.Select(dbModel =>
                            //куда                откуда
                _mapper.Map<BankAccountViewModel>(dbModel)
                )
                .ToList();
            return View(modelNew);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Cabinet(BankAccountViewModel viewModel)
        {
            int accountLifeTime;
            if ( viewModel.Currency == Currency.BYN)
            {
                viewModel.Type = "Счет";
                accountLifeTime = 5;
            }
            else
            {
                viewModel.Type = "Валютный счет";
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

            var modelNew = user.BankAccounts.Select(dbModel =>
                            //куда                откуда
                _mapper.Map<BankAccountViewModel>(dbModel)
                )
                .ToList();

            return View(modelNew);
        }
    }
}