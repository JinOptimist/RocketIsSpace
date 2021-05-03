﻿using AutoMapper;
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
            var collection = new CollectionRocketShopViewModel
            {
                //AddRockets = _mapper.Map<List<AddShopRocketViewModel>>(_shopRocketRepository.GetAll())
                AddRockets = _shopRocketRepository.GetAll()
                    .Select(x=>_mapper.Map<AddShopRocketViewModel>(x))
                    .ToList()
            };
            return View(collection);
        }

        [HttpPost]
        [Authorize]
        public IActionResult RocketShop(CollectionRocketShopViewModel collection)
        {
            if (!ModelState.IsValid)
            {
                return View(collection);
            }

            var order = _mapper.Map<Order>(collection);
            _orderRepository.Save(order);

            return View(collection);

        }

        [HttpGet]
        [IsAdmin]
        public IActionResult AdminAddRocket()
        {
            var model = new AddShopRocketViewModel();
            return View(model);
        }

        [HttpPost]
        [IsAdmin]
        public IActionResult AdminAddRocket(AddShopRocketViewModel model)
        {
            var rocket = _mapper.Map<AddShopRocket>(model);
            _shopRocketRepository.Save(rocket);
            return View();
        }
    }
}
