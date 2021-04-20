using Microsoft.EntityFrameworkCore;
using SpaceWeb.EfStuff.Model;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWeb.EfStuff.Repositories
{
    public abstract class BaseRepository<ModelType> where ModelType : BaseModel
    {
        protected SpaceDbContext _spaceDbContext;
        protected DbSet<ModelType> _dbSet;

        public BaseRepository(SpaceDbContext spaceDbContext)
        {
            _spaceDbContext = spaceDbContext;
            _dbSet = _spaceDbContext.Set<ModelType>();
        }

        public List<ModelType> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Save(ModelType model)
        {
            if (model.Id > 0)
            {
                _spaceDbContext.Update(model);
            }
            else 
            {
                _dbSet.Add(model);
            }
            _spaceDbContext.SaveChanges();
        }

        public ModelType Get(long id)
        {
            return _dbSet.SingleOrDefault(x => x.Id == id);
        }

        public void Remove(long id)
        {
            var model = Get(id);
            Remove(model);
        }

        public void Remove(ModelType model)
        {
            _dbSet.Remove(model);
            _spaceDbContext.SaveChanges();
        }
    }
}
