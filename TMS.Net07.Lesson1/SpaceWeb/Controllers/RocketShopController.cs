using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models.RocketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceWeb.Controllers.CustomAttribute;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Service;
using SpaceWeb.Models.RocketModels;
using SpaceWeb.Presentation;
using SpaceWeb.Presentation;

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
        private readonly IRocketShopPresentation _rocketShopPresentation;

        public RocketShopController(IMapper mapper, IOrderRepository orderRepository, 
            IShopRocketRepository shopRocketRepository, UserService userService, 
            IClientRepository clientRepository, ICurrencyService currencyService, IBankAccountRepository accountRepository,
            IRocketShopPresentation rocketShopPresentation)
        {
            
            _mapper = mapper;
            _orderRepository = orderRepository;
            _shopRocketRepository = shopRocketRepository;
            _userService = userService;
            _clientRepository = clientRepository;
            _currencyService = currencyService;
            _accountRepository = accountRepository;
            _rocketShopPresentation = rocketShopPresentation;
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
            var order = new Order {Rockets = rocketList, 
                OrderDateTime = DateTime.Now,
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
        public IActionResult Basket(OrderViewModel order)
        {
            return View();
        }
        
        public IActionResult PayAbilityCheck(string accountNumber,string amount)
        {
            var account = _accountRepository.Get(accountNumber);
            var result = _currencyService.CheckBalanceToPay(account, Convert.ToDecimal(amount));
            return Json(result);
        }
    }
}
