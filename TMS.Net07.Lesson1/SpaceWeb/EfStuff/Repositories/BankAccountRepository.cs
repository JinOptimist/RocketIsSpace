using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class BankAccountRepository
    {
        private SpaceDbContext _spaceDbContext;

        public BankAccountRepository(SpaceDbContext spaceDbContext)
        {
            _spaceDbContext = spaceDbContext;
        }
        
        public List <BankAccount> GetAll()
        {
            return _spaceDbContext.BankAccount.ToList();
        }

        public BankAccount Get (string BankAccountId)
        {
            return _spaceDbContext.BankAccount
                .SingleOrDefault(account => account.BankAccountId == BankAccountId);
        }

        public void Save (BankAccount model)
        {
            _spaceDbContext.BankAccount.Add(model);
            _spaceDbContext.SaveChanges();
        }

        public void Remove(BankAccount model)
        {
            _spaceDbContext.BankAccount.Remove(model);
            _spaceDbContext.SaveChanges();
        }

        public void Remove (string BankAccountId)
        {
            var modelToRemove = Get(BankAccountId);
            Remove(modelToRemove);
        }

    }
}
