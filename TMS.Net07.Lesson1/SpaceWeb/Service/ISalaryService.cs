using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;

namespace SpaceWeb.Service
{
    public interface ISalaryService
    {
        decimal CalculateAccrual(DateTime date, Employe employe);
        void Payment();
        Accrual GetAccrualInAMonth(long employeId, DateTime date);
        void GetPayedPaymentsBeforeDate(long employeId, DateTime date);
        List<Accrual> GetAllAccruals(long employeId);
        List<Payment> GetAllPayments(long employeId);
        List<DateTime> PickUpMonths(DateTime start, DateTime end, List<DateTime> accruals);
        decimal GetIndebtedness(long employeId);
        decimal GetPayedSalary(long employeId);
    }
}