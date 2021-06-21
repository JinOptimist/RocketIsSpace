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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using SpaceWeb.Models.Chart;
using SpaceWeb.Extensions;
using System;

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
        private IWebHostEnvironment _hostEnvironment;
        private UserService _userService;


        public HumanController(
            IUserRepository userRepository,
            IMapper mapper,
            IDepartmentRepository departmentRepository,
            IHumanPresentation humanPresentation,
            IEmployeRepository employeRepository,
            IWebHostEnvironment hostEnvironment,
            UserService userService 
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _humanPresentation = humanPresentation;
            _employeRepository = employeRepository;
            _hostEnvironment = hostEnvironment;
            _userService = userService; 
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
        public IActionResult PersonnelSubmit(PersonnelViewModel personnelViewModel)
        {
            _humanPresentation.SavePersonnelChanges(personnelViewModel.RequestsToEmploy);
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

        [HttpGet]
        public IActionResult DownloadDepartments()
        {
            var webPath = _hostEnvironment.WebRootPath;
            var path = Path.Combine(webPath, "TempFile", "temp-departments.docx");
            var contentTypeDocx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            var fileName = "departments.docx";            
             _humanPresentation.SaveDepartmentsToDocX(path);
            return PhysicalFile(path, contentTypeDocx, fileName);
        }

        public IActionResult GetGraph()
        {
            return Json(_humanPresentation.GetChartForWorkersInDepartment());
        }

        public IActionResult Graph()
        {
            return View();
        }

        public IActionResult GetEmloyeAccrualsInfo(long id)
        {
            return Json(_humanPresentation.GetAccrualViewModel(id));
        }
        
        [HttpPost]
        public IActionResult SaveAccrual(AccrualViewModel accrualViewModel)
        {
            _humanPresentation.SaveAccrual(accrualViewModel);
            return RedirectToAction("Personnel");
        }
    }
}