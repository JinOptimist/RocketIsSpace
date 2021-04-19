using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;

namespace SpaceWeb.Controllers
{
    public class HumanController : Controller
    {
        private UserRepository _userRepository;
        private IMapper _mapper;
        private DepartmentRepository _departmentRepository;

        public HumanController(UserRepository userRepository, IMapper mapper, DepartmentRepository departmentRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult Profile(long id)
        {
            var user = _userRepository.Get(id);
            var model = new UserProfileViewModel()
            {
                Name = user.Name,
                SurName = user.Surname,
                Age = user.Age
            };
            return View(model);
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
                var user = new User
                {
                    Login = model.Login,
                    Password = model.Password,
                    Name = model.UserProfile.Name,
                    Surname = model.UserProfile.SurName,
                    BirthDate = model.UserProfile.BirthDate
                };
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
        public IActionResult Login(RegistrationViewModel model)
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

            return Redirect($"Profile?id={user.Id}");
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
