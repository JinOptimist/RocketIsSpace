using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models.RocketModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders.Physical;
using Novacode;
using SpaceWeb.Controllers.CustomAttribute;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Service;
using SpaceWeb.Presentation;
using SpaceWeb.Models;
using SpaceWeb.Models.Chart;

namespace SpaceWeb.Controllers
{
    public class RocketShopController : Controller
    {
        private IMapper _mapper;
        private IOrderRepository _orderRepository;
        private IShopRocketRepository _shopRocketRepository;
        private UserService _userService;
        private IClientRepository _clientRepository;
        private ICurrencyService _currencyService;
        private IBankAccountRepository _accountRepository;
        private IRocketShopPresentation _rocketShopPresentation;
        private IUserRepository _userRepository;
        private IWebHostEnvironment _hostEnvironment;

        public RocketShopController(IMapper mapper, IOrderRepository orderRepository, 
            IShopRocketRepository shopRocketRepository, UserService userService, 
            IClientRepository clientRepository, ICurrencyService currencyService, IBankAccountRepository accountRepository,
            IRocketShopPresentation rocketShopPresentation, IUserRepository userRepository,IWebHostEnvironment hostEnvironment)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _shopRocketRepository = shopRocketRepository;
            _userService = userService;
            _clientRepository = clientRepository;
            _currencyService = currencyService;
            _accountRepository = accountRepository;
            _hostEnvironment = hostEnvironment;
            _rocketShopPresentation = rocketShopPresentation;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult RocketShop()
        {
            var collection = _rocketShopPresentation.GetCollectionRocketShopViewModel();
            return View(collection);
        }

        [HttpPost]
        [Authorize]
        public IActionResult RocketShop(ComplexRocketShopViewModel model)
        {
            var rocketList = model.RocketIds.Select(rocketid => _shopRocketRepository.Get(rocketid)).ToList();

            var client = _clientRepository.Get(model.ClientId);
            var order = new Order
            {
                Rockets = rocketList,
                OrderDateTime = DateTime.Today,
                Client = client,
                State = OrderStates.Pending
            };
            foreach (var rocket in order.Rockets)
            {
                order.Price += rocket.Cost;
            }

            _orderRepository.Save(order);
            order.Name = "Заказ№" + order.Id;
            _orderRepository.Save(order);

            return RedirectToAction("RocketShop");
        }

        [HttpGet]
        [IsAdmin]
        public IActionResult AddRocket()
        {
            var model = new ShopRocketViewModel();
            return View(model);
        }

        [HttpPost]
        [IsAdmin]
        public IActionResult AddRocket(ShopRocketViewModel model)
        {
            var rocket = _mapper.Map<Rocket>(model);
            _shopRocketRepository.Save(rocket);
            return View();
        }

        [HttpGet]
        public IActionResult Basket()
        {
            var user = _userService.GetCurrent();
            var orders = user.Client.Orders
                .Where(x => x.State == OrderStates.Pending)
                .ToList();
            var orderViewModel = _mapper.Map<List<OrderViewModel>>(orders);
            var basketVM = new BasketViewModel(user.BankAccounts)
            {
                Orders = orderViewModel
            };
            return View(basketVM);
        }

        [HttpPost]
        public IActionResult Basket(BasketViewModel order)
        {
            return View();
        }

        public IActionResult PayAbilityCheck(string accountNumber, string amount)
        {
            var account = _accountRepository.Get(accountNumber);
            var result = _currencyService.CheckBalanceToPay(account, Convert.ToDecimal(amount));
            return Json(result);
        }

        public IActionResult DownloadOrderFile(string name)
        {
             var webPath = _hostEnvironment.WebRootPath;
             var path = Path.Combine(webPath, "TempFile", $"{name}.docx");
             //var imagePath = Path.Combine(webPath, "TempFile", $"{name}.jpeg");
             //var pathImage = AppDomain.CurrentDomain.BaseDirectory + "image.jpg";
             const string password = "password";
             
             var order = _orderRepository.GetByName(name);
             using (var doc = DocX.Create(path))
             {
                 doc.InsertParagraph("Hello dear customer!")
                     .Font("BankGothic Md BT")
                     .Bold()
                     .FontSize(36)
                     .Spacing(15)
                     .Alignment = Alignment.center;;
                 
                 // Image image = doc.AddImage(imagePath);
                 // Paragraph paragraph = doc.InsertParagraph();
                 // paragraph.AppendPicture(image.CreatePicture());
                 // paragraph.Alignment = Alignment.center;
                 

                 doc.InsertParagraph($"Your order: {order.Name}")
                     .FontSize(14)
                     .Spacing(1)
                     .Font("Times New Roman");
                 doc.InsertParagraph($"State: {order.State.ToString()}")
                     .FontSize(14)
                     .Spacing(1)
                     .Font("Times New Roman");;
                 doc.InsertParagraph($"Price: {order.Price}")
                     .FontSize(14)
                     .Spacing(1)
                     .Font("Times New Roman");;
                 doc.InsertParagraph($"Date: {order.OrderDateTime}")
                     .FontSize(14)
                     .Spacing(1)
                     .Font("Times New Roman");
                 doc.InsertParagraph($"Thank you!")
                     .FontSize(14)
                     .Spacing(1)
                     .Font("Times New Roman");
                 
                 doc.AddProtection(EditRestrictions.readOnly, password);
                 doc.Save();
             }
            
             var contentTypeDocx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
             var fileName = $"{order.Name}.docx";
             return PhysicalFile(path, contentTypeDocx, fileName);
            
            // var path = "C:/Users/smuglifriend/OneDrive/Рабочий стол/RocketIsSpace/RocketIsSpace/TMS.Net07.Lesson1/Test/Smile.docx";
            // var contentTypeDocx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            // var fileNmae = "Order.docx";
            // return PhysicalFile(path, contentTypeDocx, fileNmae);
        }

        public IActionResult ChangeCurrency(string accountNumber, string amount, string currency)
        {
            var account = _accountRepository.Get(accountNumber);

            var currencyFrom = (Currency) Enum.Parse(typeof(Currency), currency);
            var money = _currencyService.ConvertByAlex(currencyFrom,
                Convert.ToDecimal(amount), account.Currency);
            return Json(new
            {
                money = money.ToString(),
                currency = AttributeService.GetDisplayValue(account.Currency)
            });
        }

        public IActionResult OrderChartInfo()
        {
            var allUsers = _userRepository.GetAll().Where(x => x.Client != null).ToList();
            var allOrders = allUsers.SelectMany(user => user.Client.Orders)
                .Where(x =>
                    x.OrderDateTime.Month == DateTime.Today.Month
                    && x.OrderDateTime.Year == DateTime.Today.Year)
                .ToList();
            var days = allOrders
                .Select(x => x.OrderDateTime.Day)
                .Distinct()
                .ToList();

            var chartViewModel = new OrderChartViewModel
            {
                Labels = days
            };
            var datasetViewModel = new OrderDatasetViewModel()
            {
                Label = $"Orders for {DateTime.Today.Month}.{DateTime.Today.Year}",
                BackgroundColor = "rgba(22, 53, 79, 0.83)",
            };
            var a = days.Select(day =>
                allOrders.Count(order => order.OrderDateTime.Day == day)).ToList();
            datasetViewModel.Data = a;
            chartViewModel.Datasets.Add(datasetViewModel);
            return Json(chartViewModel);
        }
    }
}