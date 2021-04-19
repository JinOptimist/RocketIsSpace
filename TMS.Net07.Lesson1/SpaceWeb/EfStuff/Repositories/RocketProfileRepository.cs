using System.Collections.Generic;
using System.Linq;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public class RocketProfileRepository:BaseRepository<RocketProfile>
    {
        public RocketProfileRepository(SpaceDbContext spaceDbContext) 
            : base(spaceDbContext)
        {
        }

        public RocketProfile GetByName(string userName)
        {
            return _spaceDbContext.RocketProfiles
                .SingleOrDefault(x => x.UserName == userName);
        }
    }
}