using SpaceWeb.EfStuff.Model;
using System.Collections.Generic;

namespace SpaceWeb.EfStuff.Repositories
{
    public interface IBaseRepository<ModelType> where ModelType : BaseModel
    {
        ModelType Get(long id);
        List<ModelType> GetAll();
        void Remove(long id);
        void Remove(ModelType model);
        void Save(ModelType model);
    }
}