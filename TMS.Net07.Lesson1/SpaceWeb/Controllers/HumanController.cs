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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Person()
        {
            var model = new PersonViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Person(PersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return View(model);
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
