using System.Collections.Generic;
using System.Linq;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public class RocketProfileRepository
    {
        private SpaceDbContext _spaceDbContext;

        public RocketProfileRepository(SpaceDbContext spaceDbContext)
        {
            _spaceDbContext = spaceDbContext;
        }

        public RocketProfile GetByName(string userName)
        {
            return _spaceDbContext.RocketProfiles
                .SingleOrDefault(x => x.UserName == userName);
        }
        
        public List<RocketProfile> GetAll()
        {
            return _spaceDbContext.RocketProfiles.ToList();
        }

        public void Save(RocketProfile user)
        {
            _spaceDbContext.RocketProfiles.Add(user);
            _spaceDbContext.SaveChanges();
        }
    }
}