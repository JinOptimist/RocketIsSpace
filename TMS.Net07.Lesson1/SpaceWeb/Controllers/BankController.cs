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
using SpaceWeb.Presentation;
using SpaceWeb.Models.Chart;
using System.Collections.Generic;
using System.Drawing;

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
        private ExchangeRateToUsdHistoryRepository _exchangeRateToUsdHistoryRepository;

        public BankController(IBankAccountRepository bankAccountRepository,
            ProfileRepository profileRepository,
            IUserRepository userRepository,
            IMapper mapper, UserService userService,
            BanksCardRepository banksCardRepository,
            BankPresentation bankPresentation,
            ExchangeRateToUsdHistoryRepository exchangeRateToUsdHistoryRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
            _banksCardRepository = banksCardRepository;
            _bankPresentation = bankPresentation;
            _exchangeRateToUsdHistoryRepository = exchangeRateToUsdHistoryRepository;
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
                .Select(dbModel => _mapper.Map<UserProfileViewModel>(dbModel)
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

        public IActionResult ExchangesHistoryChartInfo()
        {
            var chartViewModel = new ChartViewModel();

            chartViewModel.Labels = _exchangeRateToUsdHistoryRepository
                .GetAll()
                .Select(x => x.ExchRateDate.ToString())
                .Distinct()
                .ToList();
                        
            var datasetBynBuy = new DatasetViewModel()
            {
                Label = "BYN покупка",
                Data = _exchangeRateToUsdHistoryRepository.GetExchangeRateForChart(Currency.BYN, TypeOfExchange.Buy),
                BackgroundColor = "rgb(173, 255, 47)",
                BorderColor = "rgb(173, 255, 47)"
            };
            chartViewModel.Datasets.Add(datasetBynBuy);

            var datasetBynSell = new DatasetViewModel()
            {
                Label = "BYN продажа",
                Data = _exchangeRateToUsdHistoryRepository.GetExchangeRateForChart(Currency.BYN, TypeOfExchange.Sell),
                BackgroundColor = "rgb(0, 100, 0)",
                BorderColor = "rgb(0, 100, 0)"
            };
            chartViewModel.Datasets.Add(datasetBynSell);

            var datasetEurBuy = new DatasetViewModel()
            {
                Label = "EUR продажа",
                Data = _exchangeRateToUsdHistoryRepository.GetExchangeRateForChart(Currency.EUR, TypeOfExchange.Buy),
                BackgroundColor = "rgb(102, 205, 170)",
                BorderColor = "rgb(102, 205, 170)"
            };
            chartViewModel.Datasets.Add(datasetEurBuy);

            var datasetEurSell = new DatasetViewModel()
            {
                Label = "EUR продажа",
                Data = _exchangeRateToUsdHistoryRepository.GetExchangeRateForChart(Currency.EUR, TypeOfExchange.Sell),
                BackgroundColor = "rgb(0, 128, 128)",
                BorderColor = "rgb(0, 128, 128)"
            };
            chartViewModel.Datasets.Add(datasetEurSell);

            var datasetPlnBuy = new DatasetViewModel()
            {
                Label = "PLN продажа",
                Data = _exchangeRateToUsdHistoryRepository.GetExchangeRateForChart(Currency.PLN, TypeOfExchange.Buy),
                BackgroundColor = "rgb(205, 92, 92)",
                BorderColor = "rgb(205, 92, 92)"
            };
            chartViewModel.Datasets.Add(datasetPlnBuy);

            var datasetPlnSell = new DatasetViewModel()
            {
                Label = "PLN продажа",
                Data = _exchangeRateToUsdHistoryRepository.GetExchangeRateForChart(Currency.PLN, TypeOfExchange.Sell),
                BackgroundColor = "rgb(139, 0, 0)",
                BorderColor = "rgb(139, 0, 0)"
            };
            chartViewModel.Datasets.Add(datasetPlnSell);

            var datasetGbpBuy = new DatasetViewModel()
            {
                Label = "GBP продажа",
                Data = _exchangeRateToUsdHistoryRepository.GetExchangeRateForChart(Currency.GBP, TypeOfExchange.Buy),
                BackgroundColor = "rgb(238, 130, 238)",
                BorderColor = "rgb(238, 130, 238)"
            };
            chartViewModel.Datasets.Add(datasetGbpBuy);

            var datasetGbpSell = new DatasetViewModel()
            {
                Label = "GBP продажа",
                Data = _exchangeRateToUsdHistoryRepository.GetExchangeRateForChart(Currency.GBP, TypeOfExchange.Sell),
                BackgroundColor = "rgb(153, 50, 204)",
                BorderColor = "rgb(153, 50, 204)"
            };
            chartViewModel.Datasets.Add(datasetGbpSell);

            return Json(chartViewModel);
        }

        public IActionResult ExchangesHistory()
        {
            return View();
        }
    }
}