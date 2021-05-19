using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class InsuranceTypeRepository : BaseRepository<InsuranceType>
    {
        public InsuranceTypeRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
        }

        public InsuranceType GetType(InsuranceNameType type)
        {
            return _dbSet.SingleOrDefault(x =>
                x.InsuranceNameType == type);
        }

        public InsuranceType GetPeriod(InsurancePeriod period)
        {
            return _dbSet.SingleOrDefault(x =>
                x.InsurancePeriod == period);
        }

        public InsuranceType GetPolis(InsuranceNameType type, InsurancePeriod period)
        {
            return _dbSet.SingleOrDefault(x =>
                x.InsuranceNameType == type
                && x.InsurancePeriod == period);
        }
    }
}
