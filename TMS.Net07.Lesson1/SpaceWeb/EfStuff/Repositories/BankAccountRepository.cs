using Microsoft.EntityFrameworkCore;
using SpaceWeb.EfStuff.CustomException;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Model.Enum;
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

            if (accountFrom.Amount < 0)
            {
                throw new BankAccountException();
            }

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
    
        public List<BankAccount> GetByName(long userId, string name)
        {
            var sql = "SELECT * FROM BankAccount WHERE OwnerId = {0} AND[Name] = '{1}'";
            return _spaceDbContext.BankAccount.FromSqlRaw(sql, userId, name).ToList();
        }

        public BankAccount GetSpecifiedAccountByEmploye(long employeId, BankAccountType bankAccountType)
        {
            return _dbSet
                .FirstOrDefault(x => x.Owner.Employe.Id == employeId && x.BankAccountType == bankAccountType);
        }

        public List<BankAccount> GetDepartmentAccounts(long departmentId)
        {
            return
                _dbSet
                .Where
                    (x => x.Owner.Employe.Department.Id == departmentId
                    && x.BankAccountType == BankAccountType.Department)
                .ToList();
        }
    }
}
