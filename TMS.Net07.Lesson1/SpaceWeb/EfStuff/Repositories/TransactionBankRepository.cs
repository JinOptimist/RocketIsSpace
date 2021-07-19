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

      
        public decimal GetBankCardFrom(string AccountNumber)
        {
           return _dbSet.SingleOrDefault(x => x.BanksCardFrom.BankAccount.AccountNumber == AccountNumber).BanksCardFrom.BankAccount.Amount;
        }
        public decimal GetBankCardTo(string AccountNumber)
        {
            return _dbSet.SingleOrDefault(x => x.BanksCardTo.BankAccount.AccountNumber == AccountNumber).BanksCardTo.BankAccount.Amount;
        }
    }
}
