using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWeb.EfStuff.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext) { }

        public List<Payment> GetAllPayments(long employeId) => 
            _dbSet
                ?.Where(x => x.Employe.Id == employeId)
                .ToList();
    }
}