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
        private IUserService _userService;
        private IMapper _mapper;
        public RocketController(UserRepository userRepository,IMapper mapper,
        IUserService userService)
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
            return View("RocketProfile",viewModel);
        }
      
        public IActionResult MainPage()
        {
            return View();
        }

        [Authorize]
        public IActionResult Rocket()
        {
            return View();
        }
    }
}
