﻿using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class BankAccountRepository : BaseRepository<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(SpaceDbContext spaceDbContext) :
            base(spaceDbContext)
        {
        }

        public BankAccount Get(string AccountNumber)
        {
            return _dbSet.SingleOrDefault(x => x.AccountNumber == AccountNumber);
        }
       
        public List<BankAccount> GetBankAccounts(long userId)
        {
            return _dbSet.Where(x => x.Owner.Id == userId).ToList();
        }

        public List<Currency> GetCurrencies(long userId)
        {
            return _dbSet
                .Where(x => x.Owner.Id == userId)
                .Select(x => x.Currency)
                .Distinct()
                .ToList();
        }

        public bool Transfer(long bankAccountFromId, long bankAccountToId, decimal amount)
        {
            var accountFrom = Get(bankAccountFromId);
            var accountTo = Get(bankAccountToId);

            accountFrom.Amount -= amount;
            accountTo.Amount += amount;

            using (var transaction = _spaceDbContext.Database.BeginTransaction())
            {
                try
                {
                    Save(accountFrom);
                    Save(accountTo);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }

            return true;
        }
    }
}
