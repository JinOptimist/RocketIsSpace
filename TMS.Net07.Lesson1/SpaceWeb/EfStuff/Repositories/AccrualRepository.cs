using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;

namespace SpaceWeb.EfStuff.Repositories
{
    public class AccrualRepository : BaseRepository<Accrual>, IAccrualRepository
    {
        public AccrualRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext) { }

    }
}