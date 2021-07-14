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

        public List<DateTime> GetEmployeAccrualsDate(long employeId) =>
            _dbSet
                .Where(x => x.Employe.Id == employeId)
                .Select(x => x.Date)
                .ToList();

        public List<Accrual> GetAllAccruals(long employeId) =>
            _dbSet
                .Where(x => x.Employe.Id == employeId)
                .ToList();

        public Accrual GetExist(long employeId, DateTime date) =>
            _dbSet
                .SingleOrDefault(x => x.Employe.Id == employeId && x.Date == date);
    }
}