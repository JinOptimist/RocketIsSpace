using AutoMapper;
using Moq;
using NUnit.Framework;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using SpaceWeb.Presentation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceWeb.Test.Presentation
{
    class BankPresentationTest
    {
        private IBankPresentation _bankPresentation;
        private Mock<IProfileRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IProfileRepository>();
            _mockMapper = new Mock<IMapper>();
            _bankPresentation = new BankPresentation(
                _mockRepository.Object,
                _mockMapper.Object);
        }

        [Test]
        [TestCase(1)]
        public void GetProfileViewModel_1(long id)
        {
            var userprofile = new SpaceWeb.EfStuff.Model.Questionary()
            {
                Id = id,
                Name = "A",
                SurName = "B"
            };
            var prof = new QuestionaryViewModel()
            {
                Id = id,
                Name = "A",
                SurName = "B"
            };

            _mockRepository.Setup(x => x.Get(id)).Returns(userprofile);
            _mockMapper.Setup(x => x.Map<QuestionaryViewModel>(userprofile)).Returns(prof);

            var profile = _bankPresentation.GetProfileViewModel(id);

            _mockMapper.Verify(x => x.Map<QuestionaryViewModel>(userprofile), Times.AtLeastOnce);

            Assert.AreEqual(profile.Name, prof.Name);
        }

        [Test]
        [TestCase(0)]
        public void GetProfileViewModel_0(long id)
        {
            var userprofile = new SpaceWeb.EfStuff.Model.Questionary()
            {
                Id = id,
                Name = "A",
                SurName = "B"
            };
            var prof = new QuestionaryViewModel()
            {
                Id = id
            };

            _mockRepository.Setup(x => x.Get(id)).Returns(userprofile);
            _mockMapper.Setup(x => x.Map<QuestionaryViewModel>(userprofile)).Returns(prof);

            var profile = _bankPresentation.GetProfileViewModel(id);

            _mockMapper.Verify(x => x.Map<QuestionaryViewModel>(userprofile), Times.AtLeastOnce);

            Assert.AreEqual(profile.Name, null);
        }
    }
}
