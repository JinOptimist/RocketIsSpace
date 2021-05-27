using SpaceWeb.Models;
using SpaceWeb.Models.Human;
using System.Collections.Generic;

namespace SpaceWeb.Presentation
{
    public interface IHumanPresentation
    {
        List<ShortUserViewModel> GetViewModelForAllUsers();
        List<DepartmentViewModel> GetViewModelForAllDepartments();
        DepartmentViewModel GetViewModelForDepartment(long id);
        void Remove(List<long> userIds);
        PersonnelViewModel GetPersonnelViewModel();
    }
}