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

        public long GetExistId(long employeId, DateTime date)
        {
            var result = _dbSet.SingleOrDefault(x => x.Employe.Id == employeId && x.Date == date);
            return result == null ? 0 : result.Id;
        }
    }
}