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
            var pathImage = Path.Combine(webPath, "image/bank/exchanges.jpg");
            var exchanges = _exchangeRateToUsdHistoryRepository.GetAll();
            var countRows = exchanges.Count;
            var rawNow = 0;
            var deviderForSeparatingRaw = 10;
            Border slimLine = new Border(BorderStyle.Tcbs_single, BorderSize.one, 0, Color.Black);
            Border boldLine = new Border(BorderStyle.Tcbs_single, BorderSize.seven, 0, Color.Black);

            using (var doc = DocX.Create(path))
            {
                var pic = doc.AddImage(pathImage, "image/png").CreatePicture();
                pic.Width = 600;
                pic.Height = 150;
                doc.InsertParagraph().InsertPicture(pic);

                doc.InsertParagraph("История обменных курсов валют")
                    .Font("Comic Sans MS")
                    .Bold()
                    .FontSize(25)
                    .Alignment = Alignment.center;
                doc.InsertParagraph("");

                var table = doc.InsertTable(++countRows, 4);
                
                table.Rows[rawNow].Cells[0].Paragraphs.First().Append("Currency").Bold().FontSize(14).Italic().Alignment = Alignment.center;
                table.Rows[rawNow].Cells[1].Paragraphs.First().Append("Type of Exchange").Bold().FontSize(14).Italic().Alignment = Alignment.center;
                table.Rows[rawNow].Cells[2].Paragraphs.First().Append("Exchange Rate").Bold().FontSize(14).Italic().Alignment = Alignment.center;
                table.Rows[rawNow].Cells[3].Paragraphs.First().Append("Date").Bold().FontSize(14).Italic().Alignment = Alignment.center;

                for (int i = 0; i < 4; i++) // Set a bold border for the first raw in the table
                {
                    table.Rows[rawNow].Cells[i].FillColor = Color.OrangeRed;
                    table.Rows[rawNow].Cells[i].SetBorder(TableCellBorderType.Bottom, boldLine);
                    table.Rows[rawNow].Cells[i].SetBorder(TableCellBorderType.Left, boldLine);
                    table.Rows[rawNow].Cells[i].SetBorder(TableCellBorderType.Right, boldLine);
                }

                foreach (var exchange in exchanges) // Filling the table from DB
                {
                    rawNow++;
                    table.Rows[rawNow].Cells[0].Paragraphs.First().Append(exchange.Currency.ToString()).FontSize(12).Alignment = Alignment.center;
                    table.Rows[rawNow].Cells[1].Paragraphs.First().Append(exchange.TypeOfExch.ToString()).FontSize(12).Alignment = Alignment.center;
                    table.Rows[rawNow].Cells[2].Paragraphs.First().Append(exchange.ExchRate.ToString()).FontSize(12).Alignment = Alignment.center;
                    table.Rows[rawNow].Cells[3].Paragraphs.First().Append(exchange.ExchRateDate.ToString()).FontSize(12).Alignment = Alignment.center;

                    if (exchange.TypeOfExch == TypeOfExchange.Sell) // Change color for TypeOfExchange.Sell
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            table.Rows[rawNow].Cells[i].FillColor = Color.LightGray;
                        }
                    }

                    if (rawNow % deviderForSeparatingRaw == 0) // Add separating raw for each date in the table
                    {
                        var separatingRow = table.InsertRow(rawNow + 1);
                        for (int i = 0; i < 4; i++)
                        {
                            separatingRow.Cells[i].FillColor = Color.Black;
                        }
                        rawNow++;
                        deviderForSeparatingRaw += 11;
                    }
                }

                table.SetBorder(TableBorderType.InsideH, slimLine);
                table.SetBorder(TableBorderType.InsideV, slimLine);
                table.SetBorder(TableBorderType.Bottom, boldLine);
                table.SetBorder(TableBorderType.Top, boldLine);
                table.SetBorder(TableBorderType.Left, boldLine);
                table.SetBorder(TableBorderType.Right, boldLine);
                
                doc.Save();
            }

            var contentTypeDocx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            var fileName = $"History of exchange rates.docx";
            return PhysicalFile(path, contentTypeDocx, fileName);
        }
    }
}