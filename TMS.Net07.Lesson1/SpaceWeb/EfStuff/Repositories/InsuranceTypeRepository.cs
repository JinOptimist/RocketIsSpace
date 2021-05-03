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
    }
}
