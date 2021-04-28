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
using SpaceWeb.EfStuff.Repositories.IRepository;

namespace SpaceWeb.Controllers
{
    public class RocketController : Controller
    {
        private IUserRepository _userRepository;
        private UserService _userService;
        private IMapper _mapper;
        public RocketController(IUserRepository userRepository,IMapper mapper,
        UserService userService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
        }
        
        [Authorize]
        public IActionResult Profile()
        {
            var user = _userService.GetCurrent();
            var viewModel = _mapper.Map<RocketProfileViewModel>(user);
            return View("Profile",viewModel);
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
