using Microsoft.AspNetCore.Mvc;
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
using SpaceWeb.Models.Chart;
using System.Collections.Generic;
using SpaceWeb.Presentation;

namespace SpaceWeb.Controllers
{
    public class BankController : Controller
    {
        private IBankAccountRepository _bankAccountRepository;
        private ProfileRepository _profileRepository;
        private IMapper _mapper;
       // private IUserRepository _userRepository;
        private BanksCardRepository _banksCardRepository;
        private UserService _userService;
        private ICurrencyService _currencyService;
        private BankPresentation _bankPresentation;

        public BankController(IBankAccountRepository bankAccountRepository,
            ProfileRepository profileRepository,
            IUserRepository userRepository,
            IMapper mapper, UserService userService,
            BanksCardRepository banksCardRepository,
            ICurrencyService currencyService,
            BankPresentation bankPresentation)
        {
            _bankAccountRepository = bankAccountRepository;
            _profileRepository = profileRepository;
           // _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
            _banksCardRepository = banksCardRepository;
            _currencyService = currencyService;
            _bankPresentation = bankPresentation;
        }
        public IActionResult Index()
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
        public IActionResult BanksCard()
        {
            var userBanksCard = _userService.GetCurrent();
            var modelNew = userBanksCard.BanksCards.Select(dbModel =>
                //куда                откуда
                _mapper.Map<BanksCardViewModel>(dbModel)
                )
                .ToList();

            return View(modelNew);
        }
        public IActionResult BankCurrensyChartInfo()
        {
            var allCurrency = new List<Currency>() { Currency.BYN, Currency.USD };

            var chartViewModel = new ChartViewModel();
            chartViewModel.Labels = allCurrency.Select(x=>x.ToString()).ToList();
            var datasetViewModel = new DatasetViewModel()
            {
                Label = "Валюты"
            };
            datasetViewModel.Data =
                allCurrency.Select(валютаОдна => 
                    _currencyService.ConvertAmount(валютаОдна)
                    )
                .ToList();

            chartViewModel.Datasets.Add(datasetViewModel);

            return Json(chartViewModel);
        }

        public IActionResult ShowBanksCard(long accountId)
        {
            BanksCard banksCard = _banksCardRepository.Get(accountId);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult AddBanksCard(long accountId, EnumBankCard card)
        {
            BankAccount bankAccount = _bankAccountRepository.Get(accountId);
            if (bankAccount == null)
            {
                switch (card)
                {
                    case EnumBankCard.PayCard:
                        bankAccount = new BankAccount()
                        {
                            Currency = Currency.BYN
                        };
                        break;
                    case EnumBankCard.valueCard:
                        bankAccount = new BankAccount()
                        {
                            Currency = Currency.USD
                        };
                        break;
                    case EnumBankCard.XCard:
                        bankAccount = new BankAccount()
                        {
                            Currency = Currency.EUR
                        };
                        break;
                }
            }


            var bankCardNew = new BanksCard();
            bankCardNew.BankAccount = bankAccount;
            bankCardNew.CreationDate = DateTime.Now;
            var pinCard = new Random().Next(1, 9999).ToString(format: "D4");
            bankCardNew.PinCard = pinCard;
            _banksCardRepository.Save(bankCardNew);



            return RedirectToAction("Index");
        }

        public IActionResult AddTransaction(long transferToId)
        {


            return RedirectToAction("Index");
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

    }
}