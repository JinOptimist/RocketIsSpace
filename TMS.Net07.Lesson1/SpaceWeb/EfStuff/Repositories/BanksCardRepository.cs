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

        public BanksCard Get(string CardNumber)
        {
            return _dbSet.SingleOrDefault(x => x.CardNumber == CardNumber);
        }

        public List<BanksCard> GetByUserId(long userId)
        {
            return _dbSet.Where(x => x.BankAccount.Owner.Id == userId).ToList();
        }
    }
}
