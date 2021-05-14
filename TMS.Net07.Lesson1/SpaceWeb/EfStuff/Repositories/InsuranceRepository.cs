using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class InsuranceRepository : BaseRepository<Insurance>
    {
        public InsuranceRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
        }

        public List<Insurance> GetnsuranceViewModels()
        {
            var models = _dbSet
                .ToList();
            return models;
        }
    }
}
