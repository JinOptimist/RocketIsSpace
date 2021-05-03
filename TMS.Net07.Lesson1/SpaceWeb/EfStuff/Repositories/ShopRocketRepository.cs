using System.Collections.Generic;
using System.Linq;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public class ShopRocketRepository: BaseRepository<Rocket>
    {
        public ShopRocketRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
        }
    }
}