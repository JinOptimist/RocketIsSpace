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
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public HumanController(
            IUserRepository userRepository,
            IMapper mapper,
            IDepartmentRepository departmentRepository,
            IHumanPresentation humanPresentation,
            IEmployeRepository employeRepository,
            UserService userService)
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
            _humanPresentation.SaveDepartment(model);
            return RedirectToAction("AllDepartments");
        }

        [HttpGet]
        public IActionResult DeleteDepartment(long id)
        {
            _humanPresentation.DeleteDepartment(id);
            return RedirectToAction("AllDepartments");
        }

        [HttpGet]
        public IActionResult EditDepartment(long id)
        {
            return PartialView("Department", _humanPresentation.GetViewModelForDepartment(id));
        }

        [HttpGet]
        [IsClient]
        public IActionResult ClientPage()
        {
            return View(_humanPresentation.ClientPage());
        }

        public IActionResult UpdateEmployes(long idDepartment)
        {
            return Json(_humanPresentation.UpdateEmployes(idDepartment));
        }

        [HttpGet]
        [IsLeaderOfDepartment]
        public IActionResult Personnel()
        {
            return View(_humanPresentation.GetPersonnelViewModel());
        }

        [HttpPost]
        public IActionResult PersonnelSubmit(List<RequestViewModel> requestViewModels)
        {
            _humanPresentation.SavePersonnelChanges(requestViewModels);
            return RedirectToAction("Personnel");
        }

        [HttpGet]
        public IActionResult RequestEmploye()
        {
            return View(new RequestViewModel());
        }

        [HttpPost]
        public IActionResult RequestEmploye(RequestViewModel requestViewModel)
        {
            _humanPresentation.SaveRequestEmploye(requestViewModel);
            return RedirectToAction("Index");
        }
    }
}