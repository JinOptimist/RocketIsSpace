using AutoMapper;
using Novacode;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models.Chart;
using SpaceWeb.Models.Human;
using SpaceWeb.Service;
using System;
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
        private IAccrualRepository _accrualRepository;
        private IUserService _userService;
        private ISalaryService _salaryService;
        private IBankAccountRepository _bankAccountRepository;

        public HumanPresentation(
            IUserRepository userRepository,
            IDepartmentRepository departmentRepository,
            IMapper mapper,
            IEmployeRepository employeRepository,
            UserService userService,
            IAccrualRepository accrualRepository,
            ISalaryService salaryService, 
            IBankAccountRepository bankAccountRepository)
        {
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _employeRepository = employeRepository;
            _userService = userService;
            _accrualRepository = accrualRepository;
            _salaryService = salaryService;
            _bankAccountRepository = bankAccountRepository;
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

        public DepartmentViewModel GetViewModelForDepartment(long departmentId)
        {
            return _mapper.Map<DepartmentViewModel>(_departmentRepository.Get(departmentId));
        }

        public void Remove(List<long> userIds)
        {
            _userRepository.Remove(userIds);
        }

        public PersonnelViewModel GetPersonnelViewModel()
        {
            PersonnelViewModel personnelViewModel = new PersonnelViewModel();
            var currentDepartmentId = _userService.GetCurrent().Employe.Department.Id;
            personnelViewModel.Department = 
                _mapper.Map<DepartmentViewModel>(
                    _departmentRepository
                    .Get(currentDepartmentId));

            personnelViewModel.Department.Employes =
                _employeRepository.GetEmployesByDepartment(currentDepartmentId)
                .Select(x => _mapper.Map<ShortEmployeViewModel>(x))
                .ToList();

            personnelViewModel.RequestsToEmploy =
                _employeRepository.GetRequestsToEmploy(currentDepartmentId)
                .Select(x => _mapper.Map<RequestViewModel>(x.User))
                .ToList();
            return personnelViewModel;
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
                employeTemp.StatusDate = x.StatusDate;
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

        public List<ShortEmployeViewModel> UpdateEmployes(long departmentId)
        {
            return _employeRepository
                .GetEmployesByDepartment(departmentId)
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

        public AccrualViewModel GetAccrualViewModel(long employeId)
        {
            var accrualViewModel = new AccrualViewModel();
            var employe = _employeRepository.Get(employeId);
            var accruals = _accrualRepository.GetEmployeAccrualsDate(employeId);

            accrualViewModel.EmployeId = employeId;
            accrualViewModel.DateFrom = employe.StatusDate;
            accrualViewModel.DateTo = DateTime.Today;
            accrualViewModel.NoAccrualsDates = _salaryService.PickUpMonths(
                new DateTime(accrualViewModel.DateFrom.Year, accrualViewModel.DateFrom.Month, 1),
                new DateTime(accrualViewModel.DateTo.Year, accrualViewModel.DateTo.Month, 1),
                accruals);
            return accrualViewModel;
        }

        public void SaveAccrual(AccrualViewModel accrualViewModel)
        {
            var accrual = _accrualRepository.GetExist(accrualViewModel.EmployeId, accrualViewModel.Date);

            if (accrual != null)
            {
                accrual.Amount = accrualViewModel.Amount;
            }
            else
            {
                accrual = new Accrual()
                {
                    Date = accrualViewModel.Date,
                    Amount = accrualViewModel.Amount,
                    Employe = _employeRepository.Get(accrualViewModel.EmployeId)
                };
            }
            _accrualRepository.Save(accrual);
        }

        public decimal CalculateAccrual(DateTime date, long employeId)
        {
            return _salaryService.CalculateAccrual(date, _employeRepository.Get(employeId));
        }

        public PaymentViewModel GetPaymentViewModel(long employeId)
        {
            var paymentViewModel = new PaymentViewModel();
            paymentViewModel.EmployeId = employeId;
            paymentViewModel.Date = DateTime.Today;
            paymentViewModel.Payed = _salaryService.GetPayedSalary(employeId);
            paymentViewModel.NotPayed = _salaryService.GetIndebtedness(employeId);
            //paymentViewModel.AccountNumber = _bankAccountRepository.GetSpecifiedAccountByEmploye(employeId);
            paymentViewModel.AccountNumber = 
                _employeRepository
                .Get(employeId)
                .User
                .BankAccounts
                .FirstOrDefault(x => x.Name.Contains("salary"))
                ?.AccountNumber;
            paymentViewModel.DepartmentAccountNumber = 
                _userService
                .GetCurrent()
                .BankAccounts
                .FirstOrDefault(x => x.Name.Contains("department account"))
                ?.AccountNumber;
            return paymentViewModel;
        }

        public void SavePayment(PaymentViewModel paymentViewModel)
        {
            _salaryService.Pay(paymentViewModel);
        }
    }
}
