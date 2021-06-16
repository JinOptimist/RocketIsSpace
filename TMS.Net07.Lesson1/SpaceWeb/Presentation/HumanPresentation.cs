﻿using AutoMapper;
using Novacode;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models.Chart;
using SpaceWeb.Models.Human;
using SpaceWeb.Service;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpaceWeb.Presentation
{
    public class HumanPresentation : IHumanPresentation
    {
        private IUserRepository _userRepository;
        private IDepartmentRepository _departmentRepository;
        private IMapper _mapper;
        private IEmployeRepository _employeRepository;
        private UserService _userService;

        public HumanPresentation(
            IUserRepository userRepository,
            IDepartmentRepository departmentRepository,
            IMapper mapper,
            IEmployeRepository employeRepository,
            UserService userService
            )
        {
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _employeRepository = employeRepository;
            _userService = userService;
        }

        public List<ShortUserViewModel> GetViewModelForAllUsers()
        {
            return _userRepository
                .GetAll()
                .Select(x => _mapper.Map<ShortUserViewModel>(x))
                .ToList();
        }

        public List<DepartmentViewModel> GetViewModelForAllDepartments()
        {
            return _departmentRepository
                .GetAll()
                .Select(x => _mapper.Map<DepartmentViewModel>(x))
                .ToList();
        }

        public DepartmentViewModel GetViewModelForDepartment(long id)
        {
            return _mapper.Map<DepartmentViewModel>(_departmentRepository.Get(id));
        }

        public void Remove(List<long> userIds)
        {
            _userRepository.Remove(userIds);
        }

        public List<RequestViewModel> GetPersonnelViewModel()
        {
            List<RequestViewModel> viewModel = new List<RequestViewModel>();
            viewModel =
                _employeRepository.GetRequestsToEmploy(_userService.GetCurrent().Employe.Department)
                .Select(x => _mapper.Map<RequestViewModel>(x.User))
                .ToList();
            return viewModel;
        }

        public void SavePersonnelChanges(List<RequestViewModel> requestViewModels)
        {
            var employes = requestViewModels.Select(x => _mapper.Map<Employe>(x)).ToList();
            foreach (var x in employes)
            {
                var employeTemp = _employeRepository.Get(x.Id);
                employeTemp.Position = x.Position;
                employeTemp.SalaryPerHour = x.SalaryPerHour;
                employeTemp.EmployeStatus = x.EmployeStatus;
                _employeRepository.Save(employeTemp);
            }
        }

        public void SaveRequestEmploye(RequestViewModel requestViewModel)
        {
            var user = _userService.GetCurrent();
            var department = _departmentRepository.Get(requestViewModel.DepartmentId);
            user.Employe = new Employe()
            {
                Department = department,
                Position = requestViewModel.Position,
                SalaryPerHour = requestViewModel.SalaryPerHour
            };
            _employeRepository.Save(user.Employe);
        }


        public void SaveDepartmentsToDocX(string path)
        {
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.Create(path).Dispose();
            }
            var departments = _departmentRepository.GetAll();

            using (var doc = DocX.Create(path))
            {
                doc.InsertParagraph("All Departments:").FontSize(16d).Bold().SpacingAfter(28d).Alignment = Alignment.center;
                foreach (Department department in departments)
                {
                    var p = doc.InsertParagraph().SpacingAfter(14d);
                    p.AppendLine($"Department name: {department.DepartmentName}").FontSize(14d);
                    p.AppendLine($"Department type: {department.DepartmentSpecificationType}").FontSize(14d);
                    p.AppendLine($"Count employes: {department.Employes.Count}").FontSize(14d);
                    p.AppendLine($"Maxumum employes: {department.MaximumCountEmployes}").FontSize(14d);
                    p.AppendLine($"Working hours: {department.HourStartWorking} - {department.HourEndWorking}").FontSize(14d);
                }
                doc.Save();
            }
        }

        public void SaveDepartment(DepartmentViewModel model)
        {
            _departmentRepository.Save(_mapper.Map<Department>(model));
        }

        public void DeleteDepartment(long id)
        {
            _departmentRepository.Remove(id);
        }

        public ShortUserViewModel ClientPage()
        {
            var user = _userService.GetCurrent();
            return _mapper.Map<ShortUserViewModel>(user);
        }

        public List<ShortEmployeViewModel> UpdateEmployes(long idDepartment)
        {
            return _employeRepository
                .GetEmployesByDepartment(idDepartment)
                .Select(x => _mapper.Map<ShortEmployeViewModel>(x))
                .ToList();
        }

        public MyChartViewModel<int> GetChartForWorkersInDepartment()
        {
            var departments = _departmentRepository.GetAll();
            var chartViewModel = new MyChartViewModel<int>();
            chartViewModel.Labels = departments.Select(x => x.DepartmentName).ToList();
            chartViewModel.Datasets.Add(new MyDatasetViewModel<int>()
            {
                Label = "Сотурдники",
                Data = departments
                    .Select(x => x.Employes.Where(x => x.EmployeStatus == EmployeStatus.Accepted).Count())
                    .ToList()
            });
            return chartViewModel;
        }
    }
}
