using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace SpaceWeb.EfStuff.Repositories
{
    public class TransactionBankRepository : BaseRepository<TransactionBank>, ITransactionBankRepository
    {
        public TransactionBankRepository(SpaceDbContext spaceDbContext) : base(spaceDbContext)
        {
        }

        public TransactionBank GetCardFrom(long id)
        {
           return _dbSet.SingleOrDefault(x => x.BanksCardFrom.BankAccount.Id == id);
        }
        public TransactionBank GetAmountCardTo(long id)
        {
            return _dbSet.SingleOrDefault(x => x.BanksCardTo.BankAccount.Id == id);
        }
    }
}
