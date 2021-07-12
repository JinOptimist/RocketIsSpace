using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories.IRepository
{
    public interface ITrasactionRepository : IBaseRepository<Transaction>
    {

        public List<Transaction> GetTransactionUser(long userId);

        public List<Transaction> GetTransferAmount(decimal ammount);
        public decimal GetBankCardFrom(string AccountNumber);
        public bool Transfer(long bankCardsFromId, long bankCardsToId, decimal amount);
        public BankAccount GetBankCardTo(string AccountNumber);
    }
}
