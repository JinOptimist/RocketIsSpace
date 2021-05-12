using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class BanksCardRepository : BaseRepository<BanksCard>
    {
        public BanksCardRepository(SpaceDbContext spaceDbContext) :
            base(spaceDbContext)
        {
        }

        public List<BanksCard> Get(string bankAccount)
        {
            return _dbSet
                .Where(x => x.BankAccount.AccountNumber == bankAccount)
                .ToList();
        }
    }
}
