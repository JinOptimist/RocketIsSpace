using SpaceWeb.EfStuff.Model;
using System.Collections.Generic;

namespace SpaceWeb.EfStuff.Repositories
{
    public interface IBaseRepository<ModelType> where ModelType : BaseModel
    {
        public ModelType Get(long id);
        public List<ModelType> GetAll();
        public void Remove(long id);
        public void Remove(IEnumerable<long> id);
        public void Remove(ModelType model);
        public void Save(ModelType model);
    }
}