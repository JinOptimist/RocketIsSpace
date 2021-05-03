using SpaceWeb.Models;
using SpaceWeb.Models.Human;
using System.Collections.Generic;

namespace SpaceWeb.Presentation
{
    public interface IHumanPresentation
    {
        List<ShortUserViewModel> GetViewModelForAllUsers();
        List<DepartmentViewModel> GetViewModelForAllDepartments();
    }
}