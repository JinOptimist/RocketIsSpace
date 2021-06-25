using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;

namespace SpaceWeb.EfStuff.Repositories.IRepository
{
    public interface IAccrualRepository : IBaseRepository<Accrual>
    {
        List<DateTime> GetEmployeAccrualsDate(long employeId);
        Accrual GetExist(long employeId, DateTime date);
        List<Accrual> GetAllAccruals(long employeId);
    }
}