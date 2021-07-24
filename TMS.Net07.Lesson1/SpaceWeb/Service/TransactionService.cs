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
        private IUserService _userService;
        private IBankAccountRepository _bankAccountRepository;

        public TransactionService(IBanksCardRepository banksCardRepository,
             IUserService userService, IBankAccountRepository bankAccountRepository)
        {
            _banksCardRepository = banksCardRepository;
            _userService = userService;
            _bankAccountRepository = bankAccountRepository;
        }

      


        //public void Transfer(decimal transferAmount, long transferToId)
        //{
        //    var balance = _banksCardRepository.GetAmount(transferToId.ToString());
        //    balance += transferAmount;
        //}
        public void TransferFunds(long fromAccountId, long toAccountId, decimal transferAmount)
        {
            if (transferAmount <= 0)
            {
                throw new ApplicationException("transfer amount must be positive");
            }
            else if (transferAmount == 0)
            {
                throw new ApplicationException("invalid transfer amount");
            }

            BanksCard fromAccount = _banksCardRepository.GetCardById(fromAccountId);
            BanksCard toAccount = _banksCardRepository.GetCardById(toAccountId);

            fromAccount.BankAccount.Amount -= transferAmount;
            toAccount.BankAccount.Amount += transferAmount;

            if (fromAccount.BankAccount.Amount < transferAmount)
            {
                throw new ApplicationException("insufficient funds");
            }

            _bankAccountRepository.Save(fromAccount.BankAccount);
            _bankAccountRepository.Save(toAccount.BankAccount);
        }



    }
}
