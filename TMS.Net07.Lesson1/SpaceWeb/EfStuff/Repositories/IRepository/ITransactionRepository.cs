using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories.IRepository
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        public List<Transaction> GetTransactionByUser(long userId);

        public List<Transaction> GetTransferAmount(decimal ammount);
        public decimal GetBankCardFrom(string AccountNumber);
        public BankAccount GetBankCardTo(string AccountNumber);
    }
}
