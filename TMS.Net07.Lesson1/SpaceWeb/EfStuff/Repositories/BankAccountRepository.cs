using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class BankAccountRepository : BaseRepository<BankAccount>
    {
        public BankAccountRepository(SpaceDbContext spaceDbContext) :
            base(spaceDbContext)
        {
        }
        public BankAccount Get(string BankAccountID)
        {
            return _dbSet.SingleOrDefault(x => x.BankAccountId == BankAccountID);
        }

        public void Remove(string id)
        {
            if (model.Id > 0) {
                _spaceDbContext.BankAccount.Update(model);
            }
            else
            {
                _spaceDbContext.BankAccount.Add(model);
            }
            _spaceDbContext.SaveChanges();
        }
    }
}
