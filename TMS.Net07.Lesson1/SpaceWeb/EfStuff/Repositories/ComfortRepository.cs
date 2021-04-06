using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public class ComfortRepository
    {
        private SpaceDbContext _spaceDbContext;

        public ComfortRepository(SpaceDbContext spaceDbContext)
        {
            _spaceDbContext = spaceDbContext;
        }

        public List<Comfort> GetAll()
        {
            return _spaceDbContext.Comforts.ToList();
        }

        public void Save(Comfort model)
        {
            _spaceDbContext.Comforts.Add(model);
            _spaceDbContext.SaveChanges();
        }
    }
}
