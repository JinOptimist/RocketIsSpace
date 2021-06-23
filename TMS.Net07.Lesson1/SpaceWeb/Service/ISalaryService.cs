using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;

namespace SpaceWeb.Service
{
    public interface ISalaryService
    {
        decimal CalculateAccrual(DateTime date, Employe employe);
        void PayAccrual();
        Accrual GetAccrualInAMonth(long employeId, DateTime date);
        void GetPayedAccrualInAMonth();
        List<Accrual> GetAllAccruals(long EmployeId);
        void GetAllPayedAccruals();
        List<DateTime> PickUpMonths(DateTime start, DateTime end, List<DateTime> accruals);
    }
}