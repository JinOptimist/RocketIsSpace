using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;

namespace SpaceWeb.EfStuff.Repositories
{
    public class RocketStageRepository : BaseRepository <RocketStage>, IRocketStageRepository
    {
        public RocketStageRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
        }
    }
}
