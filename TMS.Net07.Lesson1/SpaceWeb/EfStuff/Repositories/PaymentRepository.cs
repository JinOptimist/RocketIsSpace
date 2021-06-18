using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;

namespace SpaceWeb.EfStuff.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
        }
    }
}