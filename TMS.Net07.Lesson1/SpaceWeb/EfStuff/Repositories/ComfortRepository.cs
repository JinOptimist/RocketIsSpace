using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;

namespace SpaceWeb.EfStuff.Repositories
{
    public class ComfortRepository:BaseRepository<Comfort>, IComfortRepository
    {
        public ComfortRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
        }
    }
}
