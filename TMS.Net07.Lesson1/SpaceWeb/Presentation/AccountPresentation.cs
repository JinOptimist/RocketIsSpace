using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpaceWeb.Presentation
{
    public class AccountPresentation : IAccountPresentation
    {
        private IBankAccountRepository _bankAccountRepository;
        private IMapper _mapper;
        private IWebHostEnvironment _hostEnvironment;
        private UserService _userService;
        private IGenerationService _generationService;
        private ICurrencyService _currencyService;
        private ITransactionService _transactionService;

        public AccountPresentation(IBankAccountRepository bankAccountRepository,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment,
            UserService userService,
            IGenerationService generationService,
            ICurrencyService currencyService,
            ITransactionService transactionService)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _userService = userService;
            _generationService = generationService;
            _currencyService = currencyService;
            _transactionService = transactionService;
        }

        public BankAccountViewModel GetViewModelForIndex(long id)
        {

            var user = _userService.GetCurrent();

            var index = 0;
            var allAccountsForViewModel = user.BankAccounts
                .Select(x =>
                {
                    var viewModel = _mapper.Map<BankAccountViewModel>(x);
                    viewModel.AccountIndex = index++;
                    return viewModel;
                })
                .ToList();

            var viewModel = allAccountsForViewModel.Single(x => x.Id == id);

            viewModel.UserAccounts = allAccountsForViewModel;

            return viewModel;
        }

        public List<BankAccountViewModel> GetAllViewModelsForCreation()
        {
            var user = _userService.GetCurrent();
            var index = 0;
            var allAccountsViewModels = user.BankAccounts
                ?.Select(x =>
                {
                    var viewModel = _mapper.Map<BankAccountViewModel>(x);
                    viewModel.AccountIndex = index++;
                    return viewModel;
                }).ToList() ?? new List<BankAccountViewModel>();

            return allAccountsViewModels;
        }

        public string GetJsonForRemove(long id, string password)
        {
            var user = _userService.GetCurrent();

            if (user.Password != password)
            {
                return (JsonConvert.SerializeObject(false));
            }

            _bankAccountRepository.Remove(id);
            var newUrl = ("/Account/Creation");
            var newId = user.BankAccounts?.FirstOrDefault()?.Id;
            if (newId != null)
            {
                newUrl = $"/Account/Index?id={newId}";
                return (JsonConvert.SerializeObject(newUrl));
            }
            return (JsonConvert.SerializeObject(newUrl));
        }

        public long GetCreatedAccountId(BankAccountViewModel viewModel)
        {
            int accountLifeTime;

            //var type = viewModel.Amount.GetType();

            if (viewModel.Currency == Currency.BYN) //заменить двойной if
            {
                if (viewModel.Name == null)
                {
                    viewModel.Name = "Счет";
                }
                accountLifeTime = 5;
            }
            else
            {
                if (viewModel.Name == null)
                {
                    viewModel.Name = "Валютный счет";
                }
                accountLifeTime = 3;
            }

            viewModel.AccountNumber = _generationService.GenerateAccountNumber();

            viewModel.CreationDate = DateTime.Now;

            viewModel.ExpireDate = viewModel.CreationDate.AddYears(accountLifeTime);

            var modelDB =
                _mapper.Map<BankAccount>(viewModel);

            var user = _userService.GetCurrent();

            modelDB.Owner = user;

            _bankAccountRepository.Save(modelDB);

            var id = user.BankAccounts?.
                SingleOrDefault(x => x.AccountNumber == viewModel.AccountNumber)
                .Id;

            return (long)id;
        }

        public bool AccountFreezeResult(long id)
        {
            var user = _userService.GetCurrent();

            var account = user.BankAccounts?.SingleOrDefault(x => x.Id == id);

            if (account == null)
            {
                return false;
            }

            account.IsFrozen = !account.IsFrozen;

            _bankAccountRepository.Save(account);

            return true;
        }

        public string UpdateAmountResult(long id, decimal amount)
        {
            var myReg = new Regex(@"[\d]*[.,][\d]{1,2}|[\d]*"); //излишне?

            var isMatch = myReg.IsMatch(amount.ToString());

            string result;

            if (!isMatch)
            {
                result = "wrong amount";
                return JsonConvert.SerializeObject(result);
            }

            var account = _bankAccountRepository?.Get(id);

            if(account == null || account.IsFrozen)
            {
                result = "wrong account";
                return JsonConvert.SerializeObject(result);
            }
            if (amount < 0)
            {
                result = _transactionService.Withdrawal(account, amount);
            }
            else
            {
                result = _transactionService.Deposit(account, amount);
            }

            return result;
        }

        public string GetJsonAsTransferResult(long fromAccountId, string toAccountNumber, decimal transferAmount)
        {
            var fromAccount = _bankAccountRepository?.Get(fromAccountId);

            var toAccount = _bankAccountRepository?.Get(toAccountNumber);

            if (fromAccount == null || toAccount == null )
            {
                return JsonConvert.SerializeObject("wrong account");
            }
            else if (transferAmount > fromAccount.Amount)
            {
                return JsonConvert.SerializeObject("wrong amount");
            }

            _transactionService.Transfer(fromAccount, toAccount, transferAmount);

            var newAmount = fromAccount.Currency == toAccount.Currency
                ? transferAmount
                :_currencyService
                .ConvertByAlex(fromAccount.Currency, transferAmount, toAccount.Currency);

            return JsonConvert.SerializeObject(newAmount);
        }
    }
}
