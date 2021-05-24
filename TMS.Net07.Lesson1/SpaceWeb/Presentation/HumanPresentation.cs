using AutoMapper;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models.Human;
using SpaceWeb.Service;
using System.Collections.Generic;
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
            UserService userService)
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

        public PersonnelViewModel GetPersonnelViewModel()
        {
            //Get the department, where this employe lead the department
            var department = _userService.GetCurrent().Employe.Department;
            PersonnelViewModel viewModel = new PersonnelViewModel();

            viewModel.Department = _mapper.Map<DepartmentViewModel>(department);
            var list = _employeRepository.GetEmployesByDepartment(department);
            viewModel.Department.Employes = 
                list.Select(x => _mapper.Map<ShortEmployeViewModel>(x))
                .ToList();

            viewModel.RequestsToEmploy = 
                _employeRepository.GetRequestsToEmploy(department)
                .Select(x => _mapper.Map<RequestViewModel>(x.User))
                .ToList();

            return viewModel;
        }
    }
}
