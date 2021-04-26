using AutoMapper;
using Moq;
using NUnit.Framework;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceWeb.Test.Presentation
{
    public class RelicPresentationTest
    {
        private RelicPresentation _presentation;
        private Mock<IRelicRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRelicRepository>();
            _mockMapper = new Mock<IMapper>();

            _presentation = new RelicPresentation(
                _mockRepository.Object,
                _mockMapper.Object);
        }

        [Test]
        public void GetIndexViewModels_Alow()
        {
            var relicPositive = new Relic()
            {
                Price = 123,
                RelicName = "relic1"
            };
            var relicNegative = new Relic()
            {
                Price = -10,
                RelicName = "relic2"
            };
            var relics = new List<Relic>() { relicPositive, relicNegative };
            _mockRepository.Setup(x => x.GetAll()).Returns(relics);

            var viewModels = _presentation.GetIndexViewModels();

            _mockRepository.Verify(x => x.GetAll(), Times.Once);

            _mockMapper.Verify(x => x.Map<RelicViewModel>(relicPositive), Times.Once);
            _mockMapper.Verify(x => x.Map<RelicViewModel>(relicNegative), Times.Never);

            Assert.AreEqual(1, viewModels.Count);
        }

        [Test]
        [TestCase(10, false)]
        [TestCase(-30, true)]
        [TestCase(0, true)]
        public void GetIndexViewModels_CheckNegativePrice(int price, bool isFiltered)
        {
            var relic = new Relic()
            {
                Price = price,
                RelicName = "relic"
            };
            var relics = new List<Relic>() { relic };
            _mockRepository.Setup(x => x.GetAll()).Returns(relics);

            var viewModels = _presentation.GetIndexViewModels();
            var expectedCount = isFiltered ? 0 : 1;
            Assert.AreEqual(expectedCount, viewModels.Count);
        }
    }
}
