﻿using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using System.Linq;
using AutoMapper;
using Profile = SpaceWeb.EfStuff.Model.Profile;
using SpaceWeb.Service;
using Microsoft.AspNetCore.Authorization;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Presentation;

namespace SpaceWeb.Controllers
{
    public class BankController : Controller
    {
        private IBankAccountRepository _bankAccountRepository;
        private ProfileRepository _profileRepository;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private BanksCardRepository _banksCardRepository;
        private UserService _userService;
        private BankPresentation _bankPresentation;

        public BankController(IBankAccountRepository bankAccountRepository,
            ProfileRepository profileRepository,
            IUserRepository userRepository,
            IMapper mapper, UserService userService, BanksCardRepository banksCardRepository, BankPresentation bankPresentation)
        {
            _bankAccountRepository = bankAccountRepository;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
            _banksCardRepository = banksCardRepository;
            _bankPresentation = bankPresentation;
        }
        public IActionResult Index()
        {
            var input = new RegistrationViewModel();

            return View(input);
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
        public IActionResult BanksCard()
        {
            /*var bankscard = new BanksCardViewModel();
             return View(bankscard);*/
            var bankscard = _userService.GetCurrent();
             var modelNew = bankscard.BanksCards.Select(dbModel =>
                 //куда                откуда
                 _mapper.Map<BanksCardViewModel>(dbModel)
                 )
                 .ToList();
             return View(modelNew);
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
            var profile = _bankPresentation.GetProfileViewModel(id);
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
            var userprofile = _mapper.Map<Profile>(model);
            /*= new Profile()
            {
                Name = model.Name,
                SurName = model.SurName,
                BirthDate = model.BirthDate,
                Sex = model.Sex,
                PhoneNumber = model.PhoneNumber,
                PostAddress = model.PostAddress,
                IdentificationPassport = model.IdentificationPassport
            };*/

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
        //[IsBankClientOrHigher]
        [HttpGet]
        public IActionResult Cabinet()
        {
            return View();
        }
    }
}