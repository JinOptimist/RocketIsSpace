using Microsoft.EntityFrameworkCore;
using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(SpaceDbContext spaceDbContext)
            : base(spaceDbContext)
        {
        }

        public User Get(string name)
        {
            return _dbSet.SingleOrDefault(x => x.Name.ToLower() == name.ToLower() || x.UserName.ToLower()==name.ToLower());
        }
        public User GetByLogin(string login)
        {
            return _dbSet.SingleOrDefault(x => x.Login == login);
        }
    }
}
