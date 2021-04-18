using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public class OrderRepository:BaseRepository<Order>
    {
        public OrderRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
            
        }
    }
}