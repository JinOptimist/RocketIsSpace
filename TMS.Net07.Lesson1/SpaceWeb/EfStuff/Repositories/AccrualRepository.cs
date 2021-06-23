using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWeb.EfStuff.Repositories
{
    public class AccrualRepository : BaseRepository<Accrual>, IAccrualRepository
    {
        public AccrualRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext) { }

        public List<DateTime> GetEmployeAccruals(long EmployeId)
        {
            return _dbSet
                .Where(x => x.Employe.Id == EmployeId)
                .Select(x => x.Date)
                .ToList();
        }

        public List<Accrual> GetAllAccruals(long EmployeId)
        {
            return _dbSet
                .Where(x => x.Employe.Id == EmployeId)
                .ToList();
        }

        public Accrual GetExist(long employeId, DateTime date)
        {
            return _dbSet.SingleOrDefault(x => x.Employe.Id == employeId && x.Date == date);
        }
    }
}