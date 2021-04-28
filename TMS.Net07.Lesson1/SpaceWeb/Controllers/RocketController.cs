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
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models.RocketModels;
using SpaceWeb.Service;

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

        [Authorize]
        public IActionResult Rocket()
        {
            return View();
        }
    }
}
