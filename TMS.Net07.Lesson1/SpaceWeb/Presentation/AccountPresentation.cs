using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
