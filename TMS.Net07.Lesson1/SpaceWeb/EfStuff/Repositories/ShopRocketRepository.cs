using System.Collections.Generic;
using System.Linq;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;

namespace SpaceWeb.EfStuff.Repositories
{
    public class ShopRocketRepository: BaseRepository<Rocket>, IShopRocketRepository
    {
        public ShopRocketRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
        }
    }
}