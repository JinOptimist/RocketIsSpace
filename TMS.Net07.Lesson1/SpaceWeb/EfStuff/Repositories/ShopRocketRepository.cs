using System.Collections.Generic;
using System.Linq;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public class ShopRocketRepository
    {
        private SpaceDbContext _dbContext;

        public ShopRocketRepository(SpaceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AddShopRocket> GetAll()
        {
            return _dbContext.ShopRocket.ToList();
        }

        public void Save(AddShopRocket item)
        {
            _dbContext.ShopRocket.Add(item);
            _dbContext.SaveChanges();
        }
    }
}