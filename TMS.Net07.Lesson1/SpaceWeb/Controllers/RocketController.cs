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
        private AdditionRepository _additionRepository;
        public RocketController(RocketProfileRepository rocketProfileRepository,
            ComfortRepository comfortRepository, IMapper mapper, OrderRepository orderRepository,
        AdditionRepository additionRepository)
        {
            _rocketProfileRepository = rocketProfileRepository;
            _comfortRepository = comfortRepository;
            _mapper = mapper;
            _additionRepository = additionRepository;
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
                return View("Comfort/ComfortPage", viewModel);
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

        [HttpGet]
        public IActionResult Login()
        {
            var model = new RocketLoginViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(RocketLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _rocketProfileRepository.GetByName(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError(
                    nameof(RocketProfileViewModel.UserName),
                    "Нет такого пользователя");
                return View(model);
            }

            if (user.Password != model.Password)
            {
                ModelState.AddModelError(
                    nameof(RocketProfileViewModel.Password),
                    "Не правильный пароль");
                return View(model);
            }

            return RedirectToAction("MainPage", "Rocket");
        }

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

            var isUserUniq = _rocketProfileRepository.GetByName(model.UserName) == null;
            if (isUserUniq)
            {
                var user = _mapper.Map<RocketProfile>(model);
                _rocketProfileRepository.Save(user);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ChangeName(long id=1)
        {
            var user = _rocketProfileRepository.Get(id);
            var model = new ChangeNameViewModel()
            {
                Id=id,
                OldName = user.Name
            };

            return View("Factory/ChangeName",model);
        }
        
        [HttpPost]
        public IActionResult ChangeName(ChangeNameViewModel viewModel)
        {
            var user = _rocketProfileRepository.Get(viewModel.Id);
            user.Name = viewModel.NewName;
            _rocketProfileRepository.Save(user);
            return View("Factory/ChangeName",viewModel);
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

        [HttpGet]
        public IActionResult AdditionPage()
        {
            var model = new AdditionFormViewModel();
            return View("Addition/AdditionPage", model);
        }

        [HttpPost]
        public IActionResult AdditionPage(AdditionFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Addition/AdditionPage", model);
            }

            var addition = new Addition()
            {
                RescueCapsuleCount = model.RescueCapsuleCount,
                RestRoomCount = model.RestRoomCount,
                Id = model.Id,
                BotanicalCenterCount = model.BotanicalCenterCount,
                ObservarionDeckCount = model.ObservarionDeckCount
            };

            _additionRepository.Save(addition);
            return View("Comfort/ComfortPage");
        }
    }
}
