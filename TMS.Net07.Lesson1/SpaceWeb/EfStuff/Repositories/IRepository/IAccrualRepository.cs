using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;

namespace SpaceWeb.EfStuff.Repositories.IRepository
{
    public interface IAccrualRepository : IBaseRepository<Accrual>
    {
        List<DateTime> GetEmployeAccruals(long EmployeId);
        Accrual GetExist(long employeId, DateTime date);
        List<Accrual> GetAllAccruals(long EmployeId);
    }
}