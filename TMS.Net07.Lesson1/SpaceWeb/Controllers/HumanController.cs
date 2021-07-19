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
using SpaceWeb.EfStuff.CustomException;

namespace SpaceWeb.Controllers
{
    public class HumanController : Controller
    {
        private IHumanPresentation _humanPresentation;
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IDepartmentRepository _departmentRepository;
        private IEmployeRepository _employeRepository;
        private IWebHostEnvironment _hostEnvironment;
        private IUserService _userService;


        public HumanController(
            IUserRepository userRepository,
            IMapper mapper,
            IDepartmentRepository departmentRepository,
            IHumanPresentation humanPresentation,
            IEmployeRepository employeRepository,
            IWebHostEnvironment hostEnvironment,
            IUserService userService 
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
        public IActionResult DeleteDepartment(long departmentId)
        {
            _humanPresentation.DeleteDepartment(departmentId);
            return RedirectToAction("AllDepartments");
        }

        [HttpGet]
        public IActionResult EditDepartment(long departmentId)
        {
            return PartialView("Department", _humanPresentation.GetViewModelForDepartment(departmentId));
        }

        [HttpGet]
        [IsClient]
        public IActionResult ClientPage()
        {
            return View(_humanPresentation.ClientPage());
        }

        public IActionResult UpdateEmployes(long departmentId)
        {
            return Json(_humanPresentation.UpdateEmployes(departmentId));
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

        [HttpGet]
        public IActionResult GetEmloyeAccrualsInfo(long employeId)
        {
            return Json(_humanPresentation.GetAccrualViewModel(employeId));
        }
        
        [HttpPost]
        public IActionResult SaveAccrual(AccrualViewModel accrualViewModel)
        {
            _humanPresentation.SaveAccrual(accrualViewModel);
            return RedirectToAction("Personnel");
        }

        public IActionResult ChangeDate(DateTime date, long employeId)
        {
            return Json(_humanPresentation.CalculateAccrual(date, employeId));
        }

        public IActionResult GetEmployePaymentInfo(long employeId)
        {
            return Json(_humanPresentation.GetPaymentViewModel(employeId));
        }

        public IActionResult SavePayment(PaymentViewModel paymentViewModel)
        {
            try
            {
                _humanPresentation.SavePayment(paymentViewModel);   
            }
            catch (BankAccountException) { return RedirectToAction("Index", "Bank"); }
            return RedirectToAction("Personnel");
        }
    }
}