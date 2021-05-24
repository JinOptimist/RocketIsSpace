using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using Microsoft.AspNetCore.Authorization;
using SpaceWeb.Models;
using SpaceWeb.Presentation;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System.Linq;
using SpaceWeb.Service;
using SpaceWeb.Models.Human;
using System.Collections.Generic;
using SpaceWeb.Controllers.CustomAttribute;

namespace SpaceWeb.Controllers
{
    [Localize]
    public class HumanController : Controller
    {
        private IHumanPresentation _humanPresentation;
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IDepartmentRepository _departmentRepository;
        private IEmployeRepository _employeRepository;
        private UserService _userService;

        public HumanController(IUserRepository userRepository, IMapper mapper, IDepartmentRepository departmentRepository, IHumanPresentation humanPresentation, IEmployeRepository employeRepository, UserService userService = null)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _humanPresentation = humanPresentation;
            _userService = userService;
            _employeRepository = employeRepository;
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

        public IActionResult Remove(List<long> userIds)
        {
            _humanPresentation.Remove(userIds);
            return RedirectToAction("AllUsers");
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

        [HttpGet]
        [Authorize]
        public IActionResult ClientPage()
        {
            var user = _userService.GetCurrent();
            var userViewModel = _mapper.Map<ShortUserViewModel>(user);
            return View(userViewModel);
        }

        public IActionResult UpdateEmployes(long idDepartment)
        {
            var employes = _employeRepository.
                GetEmployesByDepartment(idDepartment).
                Select(x => _mapper.Map<ShortEmployeViewModel>(x)).
                ToList();
            return Json(employes);
        }

        [HttpGet]
        [IsLeaderOfDepartment]
        public IActionResult Personnel()
        {
            return View(_humanPresentation.GetPersonnelViewModel());
        }

        [HttpPost]
        public IActionResult PersonnelSubmit(PersonnelViewModel viewModel)
        {
            //not working
            var employes = viewModel.RequestsToEmploy.Select(x => _mapper.Map<Employe>(x)).ToList();
            foreach (var x in employes)
                _employeRepository.Save(x);
            return View();
        }
    }
}