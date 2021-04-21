using Microsoft.AspNetCore.Mvc;
using SpaceWeb.Controllers.CustomAttribute;
﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models.RocketModels;
using SpaceWeb.Service;

namespace SpaceWeb.Controllers
{
    public class RocketController : Controller
    {
        private UserRepository _userRepository;
        private UserService _userService;
        private OrderRepository _orderRepository;
        private IMapper _mapper;
        private ShopRocketRepository _shopRocketRepository;
        public RocketController(UserRepository userRepository,IMapper mapper,
            OrderRepository orderRepository,ShopRocketRepository shopRocketRepository,
        UserService userService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _shopRocketRepository = shopRocketRepository;
            _userService = userService;
        }
        
        [Authorize]
        public IActionResult Profile()
        {
            var user = _userService.GetCurrent();
            var viewModel = _mapper.Map<RocketProfileViewModel>(user);
            return View("Profile",viewModel);
        }
       
        [HttpGet]
        public IActionResult Login()
        {
            var model = new RocketLoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(RocketLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userRepository.Get(model.UserName);

            if (user == null)
            {
                return View(model);
            }

            if (user.Password != model.Password)
            {
                return View(model);
            }
            
            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(
                ClaimTypes.AuthenticationMethod,
                Startup.AuthMethod));
            var claimsIdentity = new ClaimsIdentity(claims, Startup.AuthMethod);
            var principal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Profile", "Rocket");
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("MainPage", "Rocket");
        }

        public IActionResult MainPage()
        {
            return View();
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

            var isUserUniq = _userRepository.Get(model.UserName) == null;
            if (isUserUniq)
            {
                var user = _mapper.Map<User>(model);
                _userRepository.Save(user);
            }

            return View(model);
        }
        
        [Authorize]
        public IActionResult Rocket()
        {
            return View();
        }
    }
}
