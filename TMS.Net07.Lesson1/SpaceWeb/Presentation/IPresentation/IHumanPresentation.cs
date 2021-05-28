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
        List<RequestViewModel> GetPersonnelViewModel();
        void SavePersonnelChanges(List<RequestViewModel> requestViewModels);
        void SaveRequestEmploye(RequestViewModel requestViewModel);
        void SaveDepartmentsToDocX(string path);
        void SaveDepartment(DepartmentViewModel model);
        void DeleteDepartment(long id);
        ShortUserViewModel ClientPage();
        List<ShortEmployeViewModel> UpdateEmployes(long idDepartment);
    }
}