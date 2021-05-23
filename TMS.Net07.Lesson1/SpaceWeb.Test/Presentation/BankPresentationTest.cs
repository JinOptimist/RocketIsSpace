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
        private BankPresentation _bankPresentation;
        private Mock<IProfileRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;

        [Test]
        [TestCase(1)]
        [TestCase(0)]
        public void GetProfileViewModel(long id)
        {
            _mockRepository = new Mock<IProfileRepository>();
            _mockMapper = new Mock<IMapper>();
            _bankPresentation = new BankPresentation(
                _mockRepository.Object,
                _mockMapper.Object);

            var userprofile = new SpaceWeb.EfStuff.Model.Profile() {
                Id = id,
                Name = "A",
                SurName = "B"
            };
            _mockRepository.Setup(x => x.Get(id)).Returns(userprofile);

            var profile = _bankPresentation.GetProfileViewModel(id);

            _mockMapper.Verify(x => x.Map<UserProfileViewModel>(userprofile));

            if (id > 0)
            {
                Assert.AreEqual(profile.Name, userprofile.Name);
            }
            else
            {
                Assert.AreEqual(profile.Name, null);
            }
        }
    }
}
