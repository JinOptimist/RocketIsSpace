using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public class RocketStageRepository : BaseRepository <RocketStage>
    {
        public RocketStageRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
        }
    }
}
