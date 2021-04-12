﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models.RocketModels;

namespace SpaceWeb.Controllers
{
    public class RocketController : Controller
    {
        private RocketProfileRepository _rocketProfileRepository;
        private ComfortRepository _comfortRepository;
        private OrderRepository _orderRepository;
        private IMapper _mapper;
        public RocketController(RocketProfileRepository rocketProfileRepository,ComfortRepository comfortRepository, IMapper mapper, OrderRepository orderRepository)
        {
            _rocketProfileRepository = rocketProfileRepository;
            _comfortRepository = comfortRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult ComfortPage()
        {
            var model = new ComfortFormViewModel();
            //return View(model);
            return View("Comfort/ComfortPage", model);
        }

        [HttpPost]
        public IActionResult ComfortPage(ComfortFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Comfort/ComfortPage",viewModel);
            }

            var comfort = new Comfort()
            {
                ToiletCount = viewModel.ToiletCount,
                KitchenSeatsCount = viewModel.KitchenSeatsCount,
                StorageCapacity = viewModel.StorageCapacity,
                SleepingCapsulesCount = viewModel.SleepingCapsulesCount
            };

            _comfortRepository.Save(comfort);
            return RedirectToAction("ComfortPage");
        }
        // [HttpGet]
        // public IActionResult Login()
        // {
        //     var model = new RocketLoginViewModel();
        //     return View(model);
        // }
        //
        // [HttpPost]
        // public IActionResult Login(RocketLoginViewModel model)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return View(model);
        //     }
        //
        //     var user = RocketUsers
        //         .SingleOrDefault(x => x.UserName == model.UserName);
        //
        //     if (user == null)
        //     {
        //         ModelState.AddModelError(
        //             nameof(RocketLoginViewModel.Login),
        //             "Нет такого пользователя");
        //         return View(model);
        //     }
        //
        //     if (user.Password != model.Password)
        //     {
        //         ModelState.AddModelError(
        //             nameof(RegistrationViewModel.Password),
        //             "Не правильный праоль");
        //         return View(model);
        //     }
        //
        //     return RedirectToAction("Profile", "User");
        // }
        public IActionResult MainPage()
        {
            return View("Factory/MainPage");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            var model = new RocketRegistrationViewModel();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Registration(RocketRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isUserUniq = _rocketProfileRepository.GetByName(model.UserName)== null;
            if (isUserUniq)
            {
                var user = _mapper.Map<RocketProfile>(model);
                _rocketProfileRepository.Save(user);
            }

            return View(model);
        }
        
        // public JsonResult IsUserExist(string name)
        // {
        //     var answer = RocketUsers.Any(x => x.UserName == name);
        //     return Json(answer);
        // }

        public IActionResult ToiletPage()
        {
            return View("Comfort/ToiletPage");
        }

        public IActionResult KitchenPage()
        {
            return View("Comfort/KitchenPage");
        }

        public IActionResult CCenterPage()
        {
            return View("Comfort/CCenterPage");
        }

        public IActionResult CapsulePage()
        {
            return View("Comfort/CapsulePage");
        }

        public IActionResult Rocket()
        {
            return View("OriginRocket/Rocket");
        }
        [HttpGet]
        public IActionResult RocketShop()
        {
            var order = new OrderViewModel();
            return View("OriginRocket/RocketShop",order);
        }
        
        [HttpPost]
        public IActionResult RocketShop(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("OriginRocket/RocketShop",orderViewModel);
            }

            var order = _mapper.Map<Order>(orderViewModel);
            _orderRepository.Save(order);
            
            return View("OriginRocket/RocketShop",orderViewModel);
        }
    }
}
