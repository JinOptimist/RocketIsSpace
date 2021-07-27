using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System.Linq;
using System.Security.Claims;
using System;
using Newtonsoft.Json;

namespace SpaceWeb.Service
{
    public class TransactionService : ITransactionService
    {
        private IBanksCardRepository _banksCardRepository;
        private IUserService _userService;
        private IBankAccountRepository _bankAccountRepository;
        private ICurrencyService _currencyService;
        private IGenerationService _generationService;
        ITransactionBankRepository _transactionBankRepository;

        public TransactionService(IBanksCardRepository banksCardRepository,
             IUserService userService,
             IBankAccountRepository bankAccountRepository,
             ICurrencyService currencyService,
             IGenerationService generationService,
             ITransactionBankRepository transactionBankRepository)
        {
            _banksCardRepository = banksCardRepository;
            _userService = userService;
            _bankAccountRepository = bankAccountRepository;
            _currencyService = currencyService;
            _generationService = generationService;
            _transactionBankRepository = transactionBankRepository;
        }

        public void Transfer(long fromAccountId,
            long toAccountId,
            decimal transferAmount)
        {
            var transferCurrency = _bankAccountRepository
                .Get(fromAccountId)
                .Currency;

            Transfer(fromAccountId, toAccountId, transferAmount, transferCurrency);
        }

        public void Transfer(BankAccount fromAccount,
            BankAccount toAccount,
            decimal transferAmount)
        {
            var transferCurrency = fromAccount.Currency;

            Transfer(fromAccount, toAccount, transferAmount, transferCurrency);
        }

        public void Transfer(long fromAccountId,
            long toAccountId,
            decimal transferAmount,
            Currency transferCurrency)
        {
            var fromAccount = _bankAccountRepository.Get(fromAccountId);

            var toAccount = _bankAccountRepository.Get(toAccountId);

            Transfer(fromAccount, toAccount, transferAmount, transferCurrency);
        }

        public void Transfer(BankAccount fromAccount,
            BankAccount toAccount,
            decimal transferAmount,
            Currency transferCurrency)
        {
            if (transferAmount <= 0)
            {
                throw new ApplicationException("transfer amount must be positive");
            }
            else if (transferAmount == 0)
            {
                throw new ApplicationException("invalid transfer amount");
            }
            else if (fromAccount.IsFrozen || toAccount.IsFrozen)
            {
                throw new ApplicationException("account is frozen");
            }

            if (fromAccount.Amount < transferAmount)
            {
                throw new ApplicationException("insufficient funds");
            }

            if (fromAccount.Currency == transferCurrency && toAccount.Currency == transferCurrency)
            {
                fromAccount.Amount -= transferAmount;

                toAccount.Amount += transferAmount;
            }
            else
            {
                var fromAmount = _currencyService
                    .ConvertByAlex(transferCurrency, transferAmount, fromAccount.Currency);

                var toAmount = _currencyService
                    .ConvertByAlex(transferCurrency, transferAmount, toAccount.Currency);

                fromAccount.Amount -= fromAmount;

                toAccount.Amount += toAmount;
            }

            var transaction = CreateTransaction(transferAmount, transferCurrency, fromAccount, toAccount);

            fromAccount.OutcomingTransactions.Add(transaction);

            toAccount.IncomingTransactions.Add(transaction);

            _transactionBankRepository.Save(transaction);

            _bankAccountRepository.Save(fromAccount);

            _bankAccountRepository.Save(toAccount);
        }

        private TransactionBank CreateTransaction(
            decimal transferAmount,
            Currency transferCurrency,
            BankAccount fromAccount = null,
            BankAccount toAccount = null)
        {
            var newTransaction = new TransactionBank
            {
                SenderAccount = fromAccount,
                ReceiverAccount = toAccount,
                TransferAmount = transferAmount,
                Currency = transferCurrency,
                CreationDate = DateTime.Now,
                TransactionNumber = _generationService.GenerateTransactionNumber(),

                //if there can be only one card - should be refactored

                BanksCardFrom = (fromAccount!=null && fromAccount.BanksCards.Any())
                ? fromAccount.BanksCards.First()
                : null,

                BanksCardTo = (toAccount != null && toAccount.BanksCards.Any())
                ? toAccount.BanksCards.First()
                : null
            };

            return newTransaction;
        }

        public string Deposit(BankAccount toAccount, decimal amount)
        {
            string result;

            if (toAccount.IsFrozen)
            {
                return JsonConvert.SerializeObject(false);
            }

            toAccount.Amount += amount;

            var transaction = CreateTransaction(amount, toAccount.Currency, toAccount: toAccount);

            toAccount.IncomingTransactions.Add(transaction);

            _transactionBankRepository.Save(transaction);

            _bankAccountRepository.Save(toAccount);

            result = JsonConvert.SerializeObject(true);

            return result;
        }

        public string Withdrawal(BankAccount fromAccount, decimal amount)
        {
            string result;

            if (fromAccount.IsFrozen)
            {
                return JsonConvert.SerializeObject(false);
            }

            if (-amount > fromAccount.Amount)
            {
                result = JsonConvert.SerializeObject("wrong amount");
            }
            else
            {
                fromAccount.Amount += amount;

                var transaction = CreateTransaction(amount, fromAccount.Currency, fromAccount: fromAccount);

                fromAccount.OutcomingTransactions.Add(transaction);

                _transactionBankRepository.Save(transaction);

                _bankAccountRepository.Save(fromAccount);

                result = JsonConvert.SerializeObject(true);
            }

            return result;
        }
    }
}
