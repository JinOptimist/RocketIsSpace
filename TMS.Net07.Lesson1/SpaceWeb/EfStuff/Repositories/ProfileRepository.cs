using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class ProfileRepository
    {
        private SpaceDbContext _spaceDbContext;

        public  ProfileRepository(SpaceDbContext spaceDbContext)
        {
            _spaceDbContext = spaceDbContext;
        }
        public List<Profile> GetAll()
        {
            return _spaceDbContext.UserProfile.ToList();
        }
      
        public void Save(Profile model)
        {
            _spaceDbContext.UserProfile.Add(model);
            _spaceDbContext.SaveChanges();
        }
        public void ChangeProfile(Profile model, string userID)
        {
            if (model.Id == 0)
            {
                _spaceDbContext.UserProfile.Add(model);
            }
            else
            {
                Profile profileToUpdate = _spaceDbContext.UserProfile
                  .Where(p => p.Id == model.Id).FirstOrDefault();

                if (profileToUpdate != null)
                {
                    _spaceDbContext.Entry(profileToUpdate).CurrentValues.SetValues(model);
                }
            }

            _spaceDbContext.SaveChanges();
        }



    }
}
