using SpaceWeb.EfStuff.Model;
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
    }
}
