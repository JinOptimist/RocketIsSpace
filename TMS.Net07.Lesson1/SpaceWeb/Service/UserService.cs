using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System.Linq;
using System.Security.Claims;
using SpaceWeb.Models.Human;

namespace SpaceWeb.Service
{
    public class UserService : IUserService
    {
        private IHttpContextAccessor _contextAccessor;
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, 
            IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }

        public bool IsClient()
        {
            var user = GetCurrent();
            return user.Client != null ? true : false;
        }

        public bool IsEmploye()
        {
            var user = GetCurrent();
            return user.Employe != null ? true : false;
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
        public bool IsAdmin()
        {
            var user = GetCurrent();
            return user.JobType == JobType.Admin;
        }

        public ClaimsPrincipal GetPrincipal(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(
                ClaimTypes.AuthenticationMethod,
                Startup.AuthMethod));
            var claimsIdentity = new ClaimsIdentity(claims, Startup.AuthMethod);
            var principal = new ClaimsPrincipal(claimsIdentity);
            return principal;
        }

        public bool IsLeader()=>
            GetCurrent()
            .Employe.Position == Position.Leader;
    }
}
