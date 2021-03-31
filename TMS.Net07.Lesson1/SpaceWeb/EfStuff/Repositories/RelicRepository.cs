using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class RelicRepository
    {
        private SpaceDbContext _spaceDbContext;

        public RelicRepository(SpaceDbContext spaceDbContext)
        {
            _spaceDbContext = spaceDbContext;
        }

        public List<Relic> GetAll()
        {
            return _spaceDbContext.Relics.ToList();
        }

        public Relic Get(long id)
        {
            return _spaceDbContext.Relics
                .SingleOrDefault(x=>x.Id == id);
        }

        public void Save(Relic model)
        {
            _spaceDbContext.Relics.Add(model);
            _spaceDbContext.SaveChanges();
        }

        public void Remove(long id)
        {
            var relic = Get(id);
            Remove(relic);
        }

        public void Remove(Relic model)
        {
            _spaceDbContext.Remove(model);
            _spaceDbContext.SaveChanges();
        }
    }
}
