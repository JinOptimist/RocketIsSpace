using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>
    {
        public DepartmentRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext) { }
    }
}
