﻿using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class BanksCardRepository : BaseRepository<BanksCard>, IBanksCardRepository
    {
        private IBankAccountRepository _bankAccountRepository;

        

        public BanksCard GetCardById(long id)
        {
            return _dbSet.SingleOrDefault(x => x.Id == id);
        }
        public BanksCardRepository(SpaceDbContext spaceDbContext,
            IBankAccountRepository bankAccountRepository) :
            base(spaceDbContext)
        {
            _bankAccountRepository = bankAccountRepository;
        }
        public BanksCard GetCard(string AccountNumber)
        {
            return _dbSet.SingleOrDefault(x => x.BankAccount.AccountNumber == AccountNumber);
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
        public List<BanksCard> GetAmount(decimal ammount)
        {
            return _dbSet
                .Where(x => x.BankAccount.Amount == ammount)
              .ToList();
        }
        public decimal GetAmount(string AccountNumber)
        {
            return _dbSet.SingleOrDefault(x => x.BankAccount.AccountNumber == AccountNumber).BankAccount.Amount;
        }
        
        public List<BanksCard> GetByUserId(long userId)
        {
            return _dbSet.Where(x => x.BankAccount.Owner.Id == userId).ToList();
        }

        public override void Remove (long id)
        {
            var cardToRemove = Get(id);
            var accountToRemove = cardToRemove.BankAccount;
            _bankAccountRepository.Remove(accountToRemove);

            base.Remove(cardToRemove);
        }
    }
}
