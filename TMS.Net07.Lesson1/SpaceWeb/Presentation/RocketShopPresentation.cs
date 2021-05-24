using AutoMapper;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Models.Human;
using SpaceWeb.Models.RocketModels;
using System.Collections.Generic;
using System.Linq;
using SpaceWeb.Service;

namespace SpaceWeb.Presentation
{
    public class RocketShopPresentation : IRocketShopPresentation
    {

        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IShopRocketRepository _shopRocketRepository;
        private readonly IUserService _userService;

        public RocketShopPresentation(IMapper mapper, IOrderRepository orderRepository,
            IShopRocketRepository shopRocketRepository, IUserService userService)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _shopRocketRepository = shopRocketRepository;
            _userService = userService;
        }


        public ComplexRocketShopViewModel GetCollectionRocketShopViewModel()
        {
            var collection = new ComplexRocketShopViewModel
            {
                AddRockets = _shopRocketRepository.GetAll()
                    .Where(x => (x.Count >= 0 && x.Cost > 0))
                    .Select(x => _mapper.Map<ShopRocketViewModel>(x))
                    .ToList(),
                ClientId = _userService.GetCurrent().Client.Id
            };
            return collection;
        }

        public void SaveRocket(ShopRocketViewModel model)
        {           
            if (model.Count >= 0 && model.Cost > 0)
            {
                var rocket = _mapper.Map<Rocket>(model);
                _shopRocketRepository.Save(rocket);
            }
        }
    }
}
