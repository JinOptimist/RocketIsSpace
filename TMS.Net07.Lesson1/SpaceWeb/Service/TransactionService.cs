using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System.Linq;
using System.Security.Claims;
using System;

namespace SpaceWeb.Service
{
    public class TransactionService : ITransactionService
    {
        private IBanksCardRepository _banksCardRepository;
        private UserService _userService;

        public TransactionService(IBanksCardRepository banksCardRepository, 
             UserService userService)
        {
            _banksCardRepository = banksCardRepository;
            _userService = userService;
        }

        private BanksCard GetCardUser(long userId)
        {
            var user = _userService.GetCurrent();
            var cards = _banksCardRepository.GetCardUser(userId).FirstOrDefault();
            if (cards == null)
            {
                throw new ApplicationException("no account exists with that id");
            }
            return cards;
        }


        //public void Transfer(decimal transferAmount, long transferToId)
        //{
        //    var balance = _banksCardRepository.GetAmount(transferToId.ToString());
        //    balance += transferAmount;
        //}
        public bool TransferFunds(int fromAccountId, int toAccountId, decimal transferAmount)
        {
            if (transferAmount <= 0)
            {
                throw new ApplicationException("transfer amount must be positive");
            }
            else if (transferAmount == 0)
            {
                throw new ApplicationException("invalid transfer amount");
            }

            BanksCard fromAccount = GetCardUser(fromAccountId);
            BanksCard toAccount = GetCardUser(toAccountId);

            fromAccount.BankAccount.Amount -= transferAmount;
            toAccount.BankAccount.Amount += transferAmount;

            if (fromAccount.BankAccount.Amount < transferAmount)
            {
                throw new ApplicationException("insufficient funds");
            }
            
            return true;
        }



    }
}
