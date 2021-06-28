﻿using AutoMapper;
using Moq;
using NUnit.Framework;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceWeb.Test.Service
{
    public class CurrencyServiceTest
    {
        private CurrencyService _currencyService;
        private Mock<IUserService> _userServiceMock;
        private Mock<IExchangeRateToUsdCurrentRepository> _exchangeRateToUsdCurrentRepositoryMock;
        private Mock<IExchangeAccountHistoryRepository> _exchangeAccountHistoryRepositoryMock;
        private Mock<IExchangeRateToUsdHistoryRepository> _exchangeRateToUsdHistoryRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _exchangeRateToUsdCurrentRepositoryMock = new Mock<IExchangeRateToUsdCurrentRepository>();
            _exchangeAccountHistoryRepositoryMock = new Mock<IExchangeAccountHistoryRepository>();
            _exchangeRateToUsdHistoryRepositoryMock = new Mock<IExchangeRateToUsdHistoryRepository>();
            _mapperMock = new Mock<IMapper>();

            _currencyService = new CurrencyService(
                _userServiceMock.Object,
                _exchangeRateToUsdCurrentRepositoryMock.Object,
                _exchangeAccountHistoryRepositoryMock.Object,
                _exchangeRateToUsdHistoryRepositoryMock.Object,
                _mapperMock.Object);
        }

        [Test]
        [TestCase(0.5, 100, 50, Currency.BYN)]
        [TestCase(2, 10, 20, Currency.USD)]
        public void ConvertByAlexTest_ToUsd(decimal rate, decimal amount, decimal expectedResult, Currency currency)
        {
            //Preparation
            var exchangeRateToUsdCurrent = new ExchangeRateToUsdCurrent()
            {
                ExchRate = rate
            };
            _exchangeRateToUsdCurrentRepositoryMock
                .Setup(x => x.GetExchangeRate(currency, TypeOfExchange.Sell))
                .Returns(exchangeRateToUsdCurrent);

            //Act
            var actualResult  = _currencyService.ConvertByAlex(amount, currency);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    
        [Test]
        public void ConvertByAlex_FromTo()
        {

        }
    }
}