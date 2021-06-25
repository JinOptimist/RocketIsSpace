using SpaceWeb.EfStuff.Model;
using System.Collections.Generic;

namespace SpaceWeb.EfStuff.Repositories.IRepository
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        List<Payment> GetAllPayments(long employeId);
    }
}