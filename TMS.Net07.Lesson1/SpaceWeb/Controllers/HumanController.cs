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
                SurName = user.SurName,
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
