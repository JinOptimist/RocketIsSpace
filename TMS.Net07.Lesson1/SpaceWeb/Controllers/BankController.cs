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
using Questionary = SpaceWeb.EfStuff.Model.Questionary;
using SpaceWeb.Service;
using Microsoft.AspNetCore.Authorization;
using SpaceWeb.Controllers.CustomAttribute;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Presentation;
using SpaceWeb.Models.Chart;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Novacode;
using Microsoft.AspNetCore.Hosting;

namespace SpaceWeb.Controllers
{
    public class BankController : Controller
    {
        private IBankAccountRepository _bankAccountRepository;
        private QuestionaryRepository _questionaryRepository;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private BanksCardRepository _banksCardRepository;
        private UserService _userService;
        private BankPresentation _bankPresentation;
        private ExchangeRateToUsdHistoryRepository _exchangeRateToUsdHistoryRepository;
        private IWebHostEnvironment _hostEnvironment;

        public BankController(IBankAccountRepository bankAccountRepository,
            QuestionaryRepository questionaryRepository,
            IUserRepository userRepository,
            IMapper mapper, UserService userService,
            BanksCardRepository banksCardRepository,
            BankPresentation bankPresentation,
            ExchangeRateToUsdHistoryRepository exchangeRateToUsdHistoryRepository,
            IWebHostEnvironment hostEnvironment)
        {
            _bankAccountRepository = bankAccountRepository;
            _questionaryRepository = questionaryRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
            _banksCardRepository = banksCardRepository;
            _bankPresentation = bankPresentation;
            _exchangeRateToUsdHistoryRepository = exchangeRateToUsdHistoryRepository;
            _hostEnvironment = hostEnvironment;
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
        public IActionResult Questionary(long id = 0)
        {
            var profile = _bankPresentation.GetProfileViewModel(id);
            return View(profile);
        }

        [HttpPost]
        public IActionResult Questionary(QuestionaryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //var user = _userRepository.Get(model.Id);
            var questionary = _mapper.Map<Questionary>(model);
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
            questionary.User = user;
            //questionary.UserRef = user.Id;

            _questionaryRepository.Save(questionary);

            return RedirectToAction("QuestionaryDataOutput");
        }

        public IActionResult QuestionaryDataOutput()
        {
            var profileDateOutput = _questionaryRepository
                .GetAll()
                .Select(dbModel => _mapper.Map<QuestionaryViewModel>(dbModel)
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

        public IActionResult AccountsChartInfo()
        {
            return View();
        }

        public IActionResult AccountsChartInfoDraw()
        {
            var user = _userService.GetCurrent();
            var currencies = user.BankAccounts.Select(x => x.Currency).Distinct();

            var chartViewModel = new ChartViewModel();
            chartViewModel.Labels = currencies.Select(x => x.ToString()).ToList();
            var datasetViewModel = new DatasetViewModel()
            {
                Label = "Валюты"
            };
            datasetViewModel.Data =
                currencies.Select(c =>
                    user.BankAccounts
                        .Where(b => b.Currency == c)
                        .Select(b => b.Amount)
                        .Sum())
                .ToList();

            chartViewModel.Datasets.Add(datasetViewModel);

            return Json(chartViewModel);
        }

        public IActionResult DownloadExchangesHistory()
        {
            var webPath = _hostEnvironment.WebRootPath;
            var user = _userService.GetCurrent();
            var path = Path.Combine(webPath, "TempFile", $"{user.Id}.docx");
            var exchanges = _exchangeRateToUsdHistoryRepository.GetAll();
            var countRows = exchanges.Count;

            using (var doc = DocX.Create(path))
            {
                doc.InsertParagraph("История обменных курсов валют")
                    .Font("Comic Sans MS")
                    .Bold()
                    .FontSize(25)
                    .Alignment = Alignment.center;
                doc.InsertParagraph("");

                var t = doc.InsertTable(++countRows, 4);
                var rawNow = 0;
                t.Rows[rawNow].Cells[0].Paragraphs.First().Append("Currency");
                t.Rows[rawNow].Cells[1].Paragraphs.First().Append("Type of Exchange");
                t.Rows[rawNow].Cells[2].Paragraphs.First().Append("Exchange Rate");
                t.Rows[rawNow].Cells[3].Paragraphs.First().Append("Date");

                foreach (var exchange in exchanges)
                {
                    rawNow++;
                    t.Rows[rawNow].Cells[0].Paragraphs.First().Append(exchange.Currency.ToString()).Color(Color.Black);
                    t.Rows[rawNow].Cells[1].Paragraphs.First().Append(exchange.TypeOfExch.ToString()).Color(Color.Black);
                    t.Rows[rawNow].Cells[2].Paragraphs.First().Append(exchange.ExchRate.ToString()).Color(Color.Black);
                    t.Rows[rawNow].Cells[3].Paragraphs.First().Append(exchange.ExchRateDate.ToString()).Color(Color.Black);
                }
                
                //t.Rows[1].Cells[1].Paragraphs[0].Append("hello").Color(Color.Green);
                //t.Rows[1].Cells[2].Width = 150;
                //t.Rows[2].Cells[2].Width = 150;
                //t.Rows[1].Cells[2].FillColor = Color.Red;

                Border simpleLine = new Border(BorderStyle.Tcbs_single, BorderSize.one, 0, Color.Black);
                //t.SetBorder(TableBorderType.InsideV, c);
                t.SetBorder(TableBorderType.InsideH, simpleLine);
                t.SetBorder(TableBorderType.InsideV, simpleLine);
                t.SetBorder(TableBorderType.Bottom, simpleLine);
                t.SetBorder(TableBorderType.Top, simpleLine);
                t.SetBorder(TableBorderType.Left, simpleLine);
                t.SetBorder(TableBorderType.Right, simpleLine);
                //Table ttt;
                //ttt.InsertRow(2);
                doc.Save();
            }

            var contentTypeDocx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            var fileName = $"{user.Id}.docx";
            return PhysicalFile(path, contentTypeDocx, fileName);
        }
    }
}