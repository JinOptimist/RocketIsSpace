using System.Linq;
using Microsoft.AspNetCore.Http;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;

namespace SpaceWeb.Service
{
    public class RocketService
    {
        private IHttpContextAccessor _contextAccessor;
        private RocketProfileRepository _rocketRepository;

        public RocketService(RocketProfileRepository rocketRepository, 
            IHttpContextAccessor contextAccessor)
        {
            _rocketRepository = rocketRepository;
            _contextAccessor = contextAccessor;
        }

        public RocketProfile GetCurrent()
        {
            var idStr = _contextAccessor.HttpContext.User
                .Claims.SingleOrDefault(x => x.Type == "Id")
                ?.Value;
            if (string.IsNullOrEmpty(idStr))
            {
                return null;
            }

            var id = long.Parse(idStr);
            return _rocketRepository.Get(id);
        }
    }
}