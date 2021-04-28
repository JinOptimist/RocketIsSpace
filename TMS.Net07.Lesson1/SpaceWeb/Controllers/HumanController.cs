using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using Microsoft.AspNetCore.Authorization;
using SpaceWeb.Models;
using SpaceWeb.Presentation;

namespace SpaceWeb.Controllers
{
    public class HumanController : Controller
    {
        private IHumanPresentation _humanPresentation;
        private UserRepository _userRepository;
        private IMapper _mapper;
        private DepartmentRepository _departmentRepository;

        public HumanController(UserRepository userRepository, IMapper mapper, DepartmentRepository departmentRepository, IHumanPresentation humanPresentation)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _humanPresentation = humanPresentation;            
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult AllUsers()
        {
            return View(_humanPresentation.GetViewModelForAllUsers());
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
