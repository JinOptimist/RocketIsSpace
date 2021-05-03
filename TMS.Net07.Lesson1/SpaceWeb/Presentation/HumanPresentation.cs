using AutoMapper;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Models.Human;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWeb.Presentation
{
    public class HumanPresentation : IHumanPresentation
    {
        private IUserRepository _userRepository;
        private IDepartmentRepository _departmentRepository;
        private IMapper _mapper;

        public HumanPresentation(IUserRepository userRepository, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
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
    }
}
