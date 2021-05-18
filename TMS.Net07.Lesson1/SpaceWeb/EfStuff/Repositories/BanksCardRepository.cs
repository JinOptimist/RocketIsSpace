using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class BanksCardRepository : BaseRepository<BanksCard>, IBanksCardRepository
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
        public List<BanksCard> GetCardUser(long userId)
        {
            return _dbSet
                .Where(x => x.BankAccount.Owner.Id == userId)
                .ToList();
                
        }
    }
}
