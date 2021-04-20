using System.Linq;
using Microsoft.AspNetCore.Http;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;

namespace SpaceWeb.Service
{
    public class RocketService
    {
        private IHttpContextAccessor _contextAccessor;
        private UserRepository _userRepository;

        public RocketService(UserRepository userRepository, 
            IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;
        }

        public User GetCurrent()
        {
            var idStr = _contextAccessor.HttpContext.User
                .Claims.SingleOrDefault(x => x.Type == "Id")
                ?.Value;
            if (string.IsNullOrEmpty(idStr))
            {
                return null;
            }

            var id = long.Parse(idStr);
            return _userRepository.Get(id);
        }
    }
}