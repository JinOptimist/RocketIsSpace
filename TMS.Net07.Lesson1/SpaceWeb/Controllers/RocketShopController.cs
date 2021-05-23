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
using SpaceWeb.Service;

namespace SpaceWeb.Controllers
{
    public class RocketShopController : Controller
    {
        private IMapper _mapper;
        private OrderRepository _orderRepository;
        private ShopRocketRepository _shopRocketRepository;
        private UserService _userService;

        public RocketShopController(IMapper mapper, OrderRepository orderRepository, 
            ShopRocketRepository shopRocketRepository, UserService userService)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _shopRocketRepository = shopRocketRepository;
            _userService = userService;
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
                //ClientId = _userService.GetCurrent().Client.Id
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

            var order = new Order {Rockets = rocketList, OrderDateTime = DateTime.Now};
            foreach (var rocket in order.Rockets)
            {
                order.Price += rocket.Cost;
            }
            order.Name = "Заказ№";
            //Client = {Id = model.ClientId}
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
            var order = new OrderViewModel();
            return View(order);
        }
        
        [HttpPost]
        public IActionResult Basket(OrderViewModel order)
        {
            
            return View();
        }
    }
}
