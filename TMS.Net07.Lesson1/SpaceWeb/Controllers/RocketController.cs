using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models.RocketModels;
using SpaceWeb.Service;

namespace SpaceWeb.Controllers
{
    public class RocketController : Controller
    {
        private IUserRepository _userRepository;
        private IUserService _userService;
        private IMapper _mapper;
        public RocketController(IUserRepository userRepository,IMapper mapper,
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
            return View("Profile",viewModel);
        }
      
        public IActionResult Index()
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
