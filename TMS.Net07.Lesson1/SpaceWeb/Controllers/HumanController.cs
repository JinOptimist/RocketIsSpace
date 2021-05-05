using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using Microsoft.AspNetCore.Authorization;
using SpaceWeb.Models;
using SpaceWeb.Presentation;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System.Linq;

namespace SpaceWeb.Controllers
{
    public class HumanController : Controller
    {
        private IHumanPresentation _humanPresentation;
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IDepartmentRepository _departmentRepository;

        public HumanController(IUserRepository userRepository, IMapper mapper, IDepartmentRepository departmentRepository, IHumanPresentation humanPresentation)
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
        public IActionResult AllDepartments()
        {
            return View(_humanPresentation.GetViewModelForAllDepartments());
        }

        [HttpPost]
        public IActionResult SaveDepartment(DepartmentViewModel model)
        {
            _departmentRepository.Save(_mapper.Map<Department>(model));
            return RedirectToAction("AllDepartments");
        }

        [HttpGet]
        public IActionResult DeleteDepartment(long id)
        {
            _departmentRepository.Remove(id);
            return RedirectToAction("AllDepartments");
        }

        [HttpGet]
        public IActionResult EditDepartment(long id)
        {
            return PartialView("Department", _humanPresentation.GetViewModelForDepartment(id));
        }
    }
}