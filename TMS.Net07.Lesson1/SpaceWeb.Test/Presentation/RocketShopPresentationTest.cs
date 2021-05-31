using AutoMapper;
using Moq;
using NUnit.Framework;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Models.RocketModels;
using SpaceWeb.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceWeb.Service;

namespace SpaceWeb.Test.Presentation
{
    public class RocketShopPresentationTest
    {
        private RocketShopPresentation _presentation;
        private Mock<IMapper> _mockMapper;
        private Mock<IOrderRepository> _mockOrderRepository;
        private Mock<IShopRocketRepository> _mockShopRocketRepository;
        private Mock<IUserService> _mockuserService;
        

        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockShopRocketRepository = new Mock<IShopRocketRepository>();
            _mockuserService = new Mock<IUserService>();
            _mockuserService.Setup(x => x.GetCurrent()).Returns(new User()
            {
                Client = new Client(){Id=1}
            });
            _presentation = new RocketShopPresentation(
                _mockMapper.Object,
                _mockOrderRepository.Object,
                _mockShopRocketRepository.Object,
                _mockuserService.Object
                );
            
        }

        [Test]
        [TestCase(10, 100, true)]
        [TestCase(0, 0.1, true)]
        [TestCase(-10, 100, false)]
        [TestCase(10, -100, false)]
        [TestCase(-10, -100, false)]
        public void GetCollectionRocketShopViewModel_CheckNegativeValues(int count, decimal cost, bool isValid)
        {
            var rocket = new Rocket()
            {
                Count = count,
                Cost = cost
            };
            var rockets = new List<Rocket>() { rocket };
            _mockShopRocketRepository.Setup(x => x.GetAll()).Returns(rockets);
            var viewModels = _presentation.GetCollectionRocketShopViewModel();
            var countExpected = isValid ? 1 : 0;
            Assert.AreEqual(countExpected, viewModels.AddRockets.Count);
        }


        [Test]
        [TestCase(10, 100, true)]
        [TestCase(0, 0.1, true)]
        [TestCase(-10, 100, false)]
        [TestCase(10, -100, false)]
        [TestCase(-10, -100, false)]
        public void SaveRocket_CheckNegativeValues(int count, double cost, bool isValid)
        {
            var rocketViewModel = new ShopRocketViewModel()
            {
                Count = count,
                Cost = cost
            };
            _presentation.SaveRocket(rocketViewModel);
            int countInvocation = _mockShopRocketRepository.Invocations.Count;
            int countExpected = isValid ? 1 : 0;
            Assert.AreEqual(countExpected, countInvocation);
        }
    }
}