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

        public AccountPresentation(IBankAccountRepository bankAccountRepository,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment,
            UserService userService)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _userService = userService;
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

            var type = viewModel.Amount.GetType();

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

            StringBuilder sb = new StringBuilder();

            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                sb.Append(rnd.Next(0, 9));
            }
            viewModel.AccountNumber = sb.ToString();

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

        public bool UpdateAmountResult(long id, decimal amount)
        {
            var myReg = new Regex(@"[\d]*[.,][\d]{1,2}|[\d]*"); //излишне?

            var isMatch = myReg.IsMatch(amount.ToString());

            if (!isMatch)
            {
                return false;
            }

            var account = _bankAccountRepository?.Get(id);

            if(account == null || account.IsFrozen)
            {
                return false;
            }

            account.Amount += amount;
            _bankAccountRepository.Save(account);

            return true;
        }
    }
}
