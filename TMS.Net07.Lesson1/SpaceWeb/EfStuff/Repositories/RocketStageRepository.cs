using System.Collections.Generic;
using System.Linq;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public class RocketStageRepository
    {
        private SpaceDbContext _spaceDbContext;

        public RocketStageRepository(SpaceDbContext spaceDbContext)
        {
            _spaceDbContext = spaceDbContext;
        }

        public List<RocketStage> GetAll()
        {
            return _spaceDbContext.RocketStages.ToList();
        }

        public void Save(RocketStage rocketStage)
        {
            _spaceDbContext.RocketStages.Add(rocketStage);
            _spaceDbContext.SaveChanges();
        }
        public RocketStage Get(long id) 
        {
            return _spaceDbContext.RocketStages
                .SingleOrDefault(x => x.Id == id);
        }
        public void Remove(long id) 
        {
            var rocketStage = Get(id);
            Remove(rocketStage);
        }
        public void Remove(RocketStage rocketStage)
        {
            _spaceDbContext.Remove(rocketStage);
            _spaceDbContext.SaveChanges();
        }
    }
}
