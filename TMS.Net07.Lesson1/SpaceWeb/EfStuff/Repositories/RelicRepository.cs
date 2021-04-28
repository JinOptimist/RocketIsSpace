using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class RelicRepository : BaseRepository<Relic>, IRelicRepository
    {
        public RelicRepository(SpaceDbContext spaceDbContext) 
            : base(spaceDbContext)
        {
        }
    }
}
