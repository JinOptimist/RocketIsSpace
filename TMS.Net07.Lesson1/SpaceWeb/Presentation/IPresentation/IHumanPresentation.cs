using Microsoft.AspNetCore.Mvc.ModelBinding;
using SpaceWeb.Models;
using SpaceWeb.Models.Chart;
using SpaceWeb.Models.Human;
using System;
using System.Collections.Generic;

namespace SpaceWeb.Presentation
{
    public interface IHumanPresentation
    {
        List<ShortUserViewModel> GetViewModelForAllUsers();
        List<DepartmentViewModel> GetViewModelForAllDepartments();
        DepartmentViewModel GetViewModelForDepartment(long departmentId);
        void Remove(List<long> userIds);
        PersonnelViewModel GetPersonnelViewModel();
        void SavePersonnelChanges(List<RequestViewModel> requestViewModels);
        void SaveRequestEmploye(RequestViewModel requestViewModel);
        void SaveDepartmentsToDocX(string path);
        void SaveDepartment(DepartmentViewModel model);
        void DeleteDepartment(long departmentId);
        ShortUserViewModel ClientPage();
        List<ShortEmployeViewModel> UpdateEmployes(long departmentId);
        MyChartViewModel<int> GetChartForWorkersInDepartment();
        AccrualViewModel GetAccrualViewModel(long employeId);
        void SaveAccrual(AccrualViewModel accrualViewModel);
        decimal CalculateAccrual(DateTime date, long employeId);
        PaymentViewModel GetPaymentViewModel(long employeId);
        bool SavePayment(PaymentViewModel paymentViewModel, out string message);
        List<string> GetErrorsStringFromModelState(ModelStateDictionary modelState);
    }
}