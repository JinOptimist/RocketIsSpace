using Microsoft.EntityFrameworkCore;
using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public abstract class BaseRepository<ModelType> 
        : IBaseRepository<ModelType> where ModelType : BaseModel
    {
        protected SpaceDbContext _spaceDbContext;
        protected DbSet<ModelType> _dbSet;

        public BaseRepository(SpaceDbContext spaceDbContext)
        {
            _spaceDbContext = spaceDbContext;
            _dbSet = _spaceDbContext.Set<ModelType>();
        }

        public virtual List<ModelType> GetAll()
        {
            return _dbSet.ToList();
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

        public virtual void Remove(ModelType model)
        {
            _spaceDbContext.Remove(model);
            _spaceDbContext.SaveChanges();
        }

        public virtual void Remove(IEnumerable<long> ids)
        {
            foreach (var userid in ids)
            {
                Remove(userid);
            }
        }
    }
}
