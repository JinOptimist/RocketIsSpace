using System.Linq;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;

namespace SpaceWeb.EfStuff.Repositories
{
    public class OrderRepository:BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
            
        }

        public Order GetByName(string name)
        {
            return _spaceDbContext.Orders.SingleOrDefault(x => x.Name == name);
        }
    }
}