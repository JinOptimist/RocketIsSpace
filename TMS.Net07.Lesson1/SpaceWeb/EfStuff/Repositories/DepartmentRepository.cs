using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System.Linq;

namespace SpaceWeb.EfStuff.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext) { }

        public Department Get(string name)
        {
            return _dbSet.SingleOrDefault(x => x.DepartmentName.ToLower() == name.ToLower());
        }
    }
}