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

namespace SpaceWeb.Test.Presentation
{
    public class RocketShopPresentationTest
    {
        private RocketShopPresentation _presentation;
        private Mock<IMapper> _mockMapper;
        private Mock<IOrderRepository> _mockOrderRepository;
        private Mock<IShopRocketRepository> _mockShopRocketRepository;


        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockShopRocketRepository = new Mock<IShopRocketRepository>();

            _presentation = new RocketShopPresentation(
                _mockMapper.Object,
                _mockOrderRepository.Object,
                _mockShopRocketRepository.Object);
        }

        [Test]
        [TestCase(10, 100, true)]
        [TestCase(0, 0.1, true)]
        [TestCase(-10, 100, false)]
        [TestCase(10, -100, false)]
        [TestCase(-10, -100, false)]
        public void GetCollectionRocketShopViewModel_CheckNegativeValues(int count, double cost, bool isValid)
        {
            var rocket = new AddShopRocket()
            {
                Count = count,
                Cost = cost
            };
            var rockets = new List<AddShopRocket>() { rocket };
            _mockShopRocketRepository.Setup(x => x.GetAll()).Returns(rockets);
            var viewModels = _presentation.GetCollectionRocketShopViewModel();
            var expectedCount = isValid ? 1 : 0;
            Assert.AreEqual(expectedCount, viewModels.AddRockets.Count);
        }

        //prototype test: todo check _mockShopRocketRepository after save object  
        public void SaveRocket_CheckNegativeValues(int count, double cost, bool iFiltered)
        {
            var rocket = new AddShopRocketViewModel()
            {
                Count = count,
                Cost = cost
            };
            _presentation.SaveRocket(rocket);
        }
    }
}