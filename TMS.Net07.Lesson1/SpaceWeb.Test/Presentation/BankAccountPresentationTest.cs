using AutoMapper;
using Moq;
using NUnit.Framework;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Presentation;
using SpaceWeb.Service;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace SpaceWeb.Test.Presentation
{
    public class BankAccountPresentationTest
    {
        private BankAccountPresentation _presentation;
        private Mock<IBankAccountRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<IUserService> _mockUserService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IBankAccountRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockUserService = new Mock<IUserService>();

            _presentation = new BankAccountPresentation(
                _mockRepository.Object,
                _mockMapper.Object,
                _mockUserService.Object);
        }

        [Test]
        public void CurrencyNotBYN()
        {
            var viewModel = new BankAccountViewModel()
            {
                Currency = Currency.BYN
            };
            var userMock = new Mock<User>();
            var bankAcc1 = new BankAccount()
            {
                AccountNumber = "1",
                Currency = Currency.BYN
            };
            var bankAcc2 = new BankAccount()
            {
                AccountNumber = "2",
                Currency = Currency.EUR
            };
            var bankAccs = new List<BankAccount>() { bankAcc1, bankAcc2 };
            userMock.Setup(x => x.BankAccounts).Returns(bankAccs);
            _mockUserService.Setup(x => x.GetCurrent()).Returns(userMock.Object);

            
            

            var models = _presentation.GetViewModelForCabinet(viewModel);

            bankAcc1.Owner = userMock.Object;
            bankAcc2.Owner = userMock.Object;
            _mockRepository.Verify(x => x.Save(bankAcc1), Times.Once);
            _mockRepository.Verify(x => x.Save(bankAcc2), Times.Once);
            

            foreach (var model in models)
            {
                var expectedType = "Счет";
                Assert.AreEqual(expectedType, model.Type);
            }
        }

        public void CheckMap()
        {
            var model = new BankAccountViewModel()
            {
                AccountNumber = "1",
                Currency = Currency.BYN,
                Type = "Счет",
                Amount = 2
            };

            _mockMapper.Verify(x => x.Map<BankAccount>(model), Times.Once);
        }
    }
}
