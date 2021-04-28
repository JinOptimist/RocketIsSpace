using Microsoft.AspNetCore.Http;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System.Linq;

namespace SpaceWeb.Service
{
    public class UserService
    {
        private IHttpContextAccessor _contextAccessor;
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, 
            IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
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

        public string GetAvatarUrl()
        {
            var userAvatar = GetCurrent()?.AvatarUrl;
            return !string.IsNullOrWhiteSpace(userAvatar) 
                ? userAvatar
                : "/image/defaultAvatar.png";
        }

        public string GetAvatarUrl(string userAvatar)
        {
            return !string.IsNullOrWhiteSpace(userAvatar)
                ? userAvatar
                : "/image/defaultAvatar.png";
        }

        public bool IsEngineer()
        {
            var user = GetCurrent();
            return user.JobType == JobType.Engineer
                || user.JobType == JobType.Admin;
        }

        public bool IsChiefBankEmployee()
        {
            var user = GetCurrent();
            return user.JobType == JobType.ChiefBankEmployee
                || user.JobType == JobType.Admin;
        }

        public bool IsBankEmployeeOrHigher()
        {
            var user = GetCurrent();
            return user.JobType == JobType.BankEmployee
                || user.JobType == JobType.ChiefBankEmployee
                || user.JobType == JobType.Admin;
        }

        public bool IsBankClientOrHigher()
        {
            var user = GetCurrent();
            return user.JobType == JobType.BankClient
                || user.JobType == JobType.BankEmployee
                || user.JobType == JobType.ChiefBankEmployee
                || user.JobType == JobType.Admin;
        }
    }
}
