using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;

namespace SpaceWeb.EfStuff.Repositories
{
    public class MazeLevelRepository: BaseRepository<MazeLevel>, IMazeLevelRepository
    {
        public MazeLevelRepository(SpaceDbContext spaceDbContext)
            : base(spaceDbContext)
        {
        }
    }
}
