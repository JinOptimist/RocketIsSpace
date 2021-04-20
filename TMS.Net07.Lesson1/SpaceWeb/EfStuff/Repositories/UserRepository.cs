using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(SpaceDbContext spaceDbContext) : base (spaceDbContext)
        {
        }
        public User GetByName(string name)
        {
            return _dbSet
                .SingleOrDefault(x => x.Name == name);
        }
    }
}