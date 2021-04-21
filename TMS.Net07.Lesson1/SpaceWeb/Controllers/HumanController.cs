using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using SpaceWeb.Service;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    public class HumanController : Controller
    {
        private UserRepository _userRepository;
        private IMapper _mapper;
        private DepartmentRepository _departmentRepository;
        private UserService _userService;

        public HumanController(UserRepository userRepository, IMapper mapper, DepartmentRepository departmentRepository, UserService userService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            var user = _userService.GetCurrent();
            var viewModel = _mapper.Map<UserProfileViewModel>(user);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            var model = new RegistrationViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isUserUniq = _userRepository.GetByLogin(model.Login) == null;
            if (isUserUniq)
            {
                var user = _mapper.Map<User>(model);
                user.Client = new Client();
                _userRepository.Save(user);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new RegistrationViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userRepository.GetByLogin(model.Login);

            if (user == null)
            {
                ModelState.AddModelError(
                    nameof(RegistrationViewModel.Login),
                    "Нет такого пользователя");
                return View(model);
            }

            if (user.Password != model.Password)
            {
                ModelState.AddModelError(
                    nameof(RegistrationViewModel.Password),
                    "Не правильный праоль");
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

            return Redirect($"Profile");
        }

        [HttpGet]
        public IActionResult Department()
        {
            var model = new DepartmentViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Department(DepartmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var department = _mapper.Map<Department>(model);
            _departmentRepository.Save(department);

            return View(model);
        }
    }
}
