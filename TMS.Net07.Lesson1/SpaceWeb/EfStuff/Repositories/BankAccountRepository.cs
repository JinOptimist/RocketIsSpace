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
            var model = Get(id);
            Remove(model);
        }
    }
}
