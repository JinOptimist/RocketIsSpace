using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using System.Linq;
using System.Security.Claims;

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
    }
}
