using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class QuestionaryRepository : BaseRepository<Questionary>, IProfileRepository
    {
        public QuestionaryRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
        }

        public void ChangeProfile(Questionary model, string userID)
        {
            if (model.Id == 0)
            {
                _dbSet.Add(model);
                //_spaceDbContext.Questionary.Add(model);
            }
            else
            {
                Questionary profileToUpdate = _dbSet
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
