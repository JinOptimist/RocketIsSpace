using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System.Linq;
using System.Security.Claims;

namespace SpaceWeb.Service
{
    public class CurrecyService
    {
        private IHttpContextAccessor _contextAccessor;
        private IBankAccountRepository _bankAccountRepository;
        private IUserRepository _userRepository;

        public CurrecyService(IBankAccountRepository bankAccountRepository,
            IHttpContextAccessor contextAccessor, IUserRepository userRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _contextAccessor = contextAccessor;
            _userRepository = userRepository;
        }

        public User GetCurrent()
        {
            var idStr = _contextAccessor.HttpContext.User
                ?.Claims.SingleOrDefault(x => x.Type == "Id")?.Value;
            if (string.IsNullOrEmpty(idStr))
            {
                return null;
            }

            var id = long.Parse(idStr);
            return _userRepository.Get(id);
        }
        public BankAccount GetAccount(int ownerId)
        {
         
            BankAccount account = _banksCardRepository.Get(accountId);

            if (account == null)
            {
                throw new ApplicationException("no account exists with that id");
            }

            return account;
        }

    }
}
