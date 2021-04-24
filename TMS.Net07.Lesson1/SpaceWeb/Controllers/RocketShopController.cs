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

namespace SpaceWeb.Controllers
{
    public class RocketShopController : Controller
    {
        private IMapper _mapper;
        private OrderRepository _orderRepository;
        private ShopRocketRepository _shopRocketRepository;

        public RocketShopController(IMapper mapper, OrderRepository orderRepository, 
            ShopRocketRepository shopRocketRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _shopRocketRepository = shopRocketRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult RocketShop()
        {
            var order = new OrderViewModel();
            return View(order);
        }

        [HttpPost]
        [Authorize]
        public IActionResult RocketShop(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(orderViewModel);
            }

            var order = _mapper.Map<Order>(orderViewModel);
            _orderRepository.Save(order);

            return View(orderViewModel);

        }

        [HttpGet]
        public IActionResult AdminAddRocket()
        {
            var model = new AdminAddRocketViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult AdminAddRocket(AdminAddRocketViewModel model)
        {
            var rocket = _mapper.Map<AddShopRocket>(model);
            _shopRocketRepository.Save(rocket);
            return View();
        }
    }
}
