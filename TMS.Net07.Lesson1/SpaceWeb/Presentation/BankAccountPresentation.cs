using AutoMapper;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWeb.Presentation
{
    public class BankAccountPresentation : IBankAccountPresentation
    {
        private IBankAccountRepository _bankAccountRepository;
        private IMapper _mapper;
        private IUserService _userService;

        public BankAccountPresentation(IBankAccountRepository bancAccountRepository, 
            IMapper mapper, IUserService userService)
        {
            _bankAccountRepository = bancAccountRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public List<BankAccountViewModel> GetViewModelForCabinet(BankAccountViewModel viewModel)
        {
            int accountLifeTime;
            if (viewModel.Currency == Currency.BYN)
            {
                viewModel.Type = "Счет";
                accountLifeTime = 5;
            }
            else
            {
                viewModel.Type = "Валютный счет";
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

            var modelNew = user.BankAccounts.Select(dbModel =>
                //куда                откуда
                _mapper.Map<BankAccountViewModel>(dbModel)
                )
                .ToList();

            return modelNew;
        }
    }
}
