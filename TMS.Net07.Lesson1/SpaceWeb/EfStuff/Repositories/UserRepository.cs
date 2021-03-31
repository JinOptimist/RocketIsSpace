using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class UserRepository
    {
        private SpaceDbContext _spaceDbContext;

        public UserRepository(SpaceDbContext spaceDbContext)
        {
            _spaceDbContext = spaceDbContext;
        }

        public User GetByName(string name)
        {
            return _spaceDbContext.Users
                .SingleOrDefault(x => x.Name == name);
        }

        public List<User> GetAll()
        {
            return _spaceDbContext.Users.ToList();
        }

        public void Save(User user)
        {
            _spaceDbContext.Users.Add(user);
            _spaceDbContext.SaveChanges();
        }
    }
}
