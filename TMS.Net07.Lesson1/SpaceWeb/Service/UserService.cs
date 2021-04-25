using Microsoft.AspNetCore.Http;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using System.Linq;

namespace SpaceWeb.Service
{
    public class UserService
    {
        private IHttpContextAccessor _contextAccessor;
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository, 
            IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }

        public User GetCurrent()
        {
            var idStr = _contextAccessor.HttpContext.User
                .Claims.SingleOrDefault(x => x.Type == "Id").Value;
            if (string.IsNullOrEmpty(idStr))
            {
                return null;
            }

            var id = long.Parse(idStr);
            return _userRepository.Get(id);
        }

        public bool IsEngineer()
        {
            var user = GetCurrent();
            return user.JobType == JobType.Engineer
                || user.JobType == JobType.Admin;
        }

        public bool IsBankPerson()
        {
            var user = GetCurrent();
            return user.JobType == JobType.ChiefBankMan
                || user.JobType == JobType.BankMan
                || user.JobType == JobType.BankClient;
        }

        public bool IsChiefBankMan()
        {
            var user = GetCurrent();
            return user.JobType == JobType.ChiefBankMan;
        }

        public bool IsBankMan()
        {
            var user = GetCurrent();
            return user.JobType == JobType.BankMan;
        }

        public bool IsBankClient()
        {
            var user = GetCurrent();
            return user.JobType == JobType.BankClient;
        }
    }
}
