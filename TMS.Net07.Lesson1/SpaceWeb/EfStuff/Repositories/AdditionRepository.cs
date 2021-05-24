using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceWeb.EfStuff.Repositories.IRepository;

namespace SpaceWeb.EfStuff.Repositories
{
    public class AdditionRepository : BaseRepository<Addition>, IAdditionRepository
    {
        public AdditionRepository(SpaceDbContext spaceDbContext)
                   : base(spaceDbContext)
        {
        }
    }
}
