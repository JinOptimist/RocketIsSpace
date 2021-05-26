using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;

namespace SpaceWeb.EfStuff.Repositories
{
    public class ClientRepository:BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
            
        }
    }
    
}