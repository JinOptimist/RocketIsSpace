using AutoMapper;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Models.Human;
using SpaceWeb.Models.RocketModels;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWeb.Presentation
{
    public class RocketShopPresentation : IRocketShopPresentation
    {

        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IShopRocketRepository _shopRocketRepository;

        public RocketShopPresentation(IMapper mapper, IOrderRepository orderRepository,
            IShopRocketRepository shopRocketRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _shopRocketRepository = shopRocketRepository;
        }


        public CollectionRocketShopViewModel GetCollectionRocketShopViewModel()
        {
            var collection = new CollectionRocketShopViewModel
            {
                AddRockets = _shopRocketRepository.GetAll()
                    .Where(x => (x.Count >= 0 && x.Cost > 0))
                    .Select(x => _mapper.Map<AddShopRocketViewModel>(x))
                    .ToList()
            };
            return collection;
        }

        public void SaveOrder(CollectionRocketShopViewModel collection)
        {
            var order = _mapper.Map<Order>(collection);
            _orderRepository.Save(order);
        }

        public void SaveRocket(AddShopRocketViewModel model)
        {
            var rocket = _mapper.Map<AddShopRocket>(model);
            _shopRocketRepository.Save(rocket);
        }
    }
}
