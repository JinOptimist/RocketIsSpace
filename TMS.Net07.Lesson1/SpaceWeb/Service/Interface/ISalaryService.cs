using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models.Human;
using System;
using System.Collections.Generic;

namespace SpaceWeb.Service
{
    public interface ISalaryService
    {
        decimal CalculateAccrual(DateTime date, Employe employe);
        bool Pay(PaymentViewModel paymentViewModel);
        Accrual GetAccrualInAMonth(long employeId, DateTime date);
        List<Accrual> GetAllAccruals(long employeId);
        List<Payment> GetAllPayments(long employeId);
        List<DateTime> PickUpMonths(DateTime start, DateTime end, List<DateTime> accruals);
        decimal GetIndebtedness(long employeId);
        decimal GetPayedSalary(long employeId);
    }
}