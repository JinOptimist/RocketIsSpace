using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(SpaceDbContext spaceDbContext) :
            base(spaceDbContext)
        {
        }

       
        public List<Transaction> GetTransactionByUser(long userId)
        {
            return _dbSet
                .Where(x => x.Owner.Id == userId)
                .ToList();
                
        }
        public List<Transaction> GetTransferAmount(decimal ammount)
        {
            return _dbSet
                .Where(x => x.TransferAmount == ammount)
              .ToList();
        }
        public decimal GetBankCardFrom(string AccountNumber)
        {
            return _dbSet.SingleOrDefault(x => x.BanksCardFrom.BankAccount.AccountNumber == AccountNumber).BanksCardFrom.BankAccount.Amount;
        }
        public BankAccount GetBankCardTo(string AccountNumber)
        {
            return _dbSet.SingleOrDefault(x => x.BanksCardTo.BankAccount.AccountNumber== AccountNumber).BanksCardTo.BankAccount;
        }
      
      


    }
}
