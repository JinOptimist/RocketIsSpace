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
    public class CurrecyService
    {
        private IHttpContextAccessor _contextAccessor;
        private IBanksCardRepository _banksCardRepository;
        private IBankAccountRepository _bankAccountRepository;

        private UserService _userService;

        public CurrecyService(IBanksCardRepository banksCardRepository,
            IHttpContextAccessor contextAccessor, UserService userService, IBankAccountRepository bankAccountRepository)
        {
            _banksCardRepository = banksCardRepository;
            _contextAccessor = contextAccessor;
            _userService = userService;
            _bankAccountRepository = bankAccountRepository;
        }

        public BanksCard GetCardUser(long userId)
        {
            var user = _userService.GetCurrent();
            var cards = _banksCardRepository.GetCardUser(userId).FirstOrDefault();
            if (cards == null)
            {
                throw new ApplicationException("no account exists with that id");
            }
            return cards;
        }
        public  void  Transfer(decimal transferAmount, long transferToId)
        {
            var balance = _banksCardRepository.GetAmount(transferToId.ToString());
            balance += transferAmount;
            //Transaction newTransaction = new Transaction(transferAmount, transferToId);
           // transactions.Add(newTransaction);
        }
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

            if (fromAccount.BankAccount.Amount < transferAmount)
            {
                throw new ApplicationException("insufficient funds");
            }

           // fromAccount.Transfer(-1 * transferAmount, toAccountId);
            //toAccount.Transfer(transferAmount, fromAccountId);

            return true;
        }


    }
}
