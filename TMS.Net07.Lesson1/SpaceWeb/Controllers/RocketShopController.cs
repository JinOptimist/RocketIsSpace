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

namespace SpaceWeb.Controllers
{
    public class RocketShopController : Controller
    {
        private IMapper _mapper;
        private IOrderRepository _orderRepository;
        private IShopRocketRepository _shopRocketRepository;
        private UserService _userService;
        private IClientRepository _clientRepository;

        public RocketShopController(IMapper mapper, IOrderRepository orderRepository, 
            IShopRocketRepository shopRocketRepository, UserService userService, 
            IClientRepository clientRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _shopRocketRepository = shopRocketRepository;
            _userService = userService;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult RocketShop()
        {
            var collection = new ComplexRocketShopViewModel
            {
                AddRockets = _shopRocketRepository.GetAll()
                    .Select(x => _mapper.Map<ShopRocketViewModel>(x))
                    .ToList(),
                ClientId = _userService.GetCurrent().Client.Id
            };
            return View(collection);
        }

        [HttpPost]
        [Authorize]
        public IActionResult RocketShop(ComplexRocketShopViewModel model)
        {
            var rocketList = new List<Rocket>();
            foreach (var rocketid in model.RocketIds)
            {
                rocketList.Add(_shopRocketRepository.Get(rocketid));
            }

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
            order.Name = "Заказ№";
            //Client = {Id = model.ClientId}
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
            return View(orderViewModel);
        }
        
        [HttpPost]
        public IActionResult Basket(OrderViewModel order)
        {
            return View();
        }
    }
}
