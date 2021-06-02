﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SpaceWeb.EfStuff;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Xml.Serialization;

namespace ExchangeRate
{
    public class Program
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            var connection = dbContextOptionsBuilder
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SpaceWeb;Trusted_Connection=True");

            SpaceDbContext spaceDbContext = new SpaceDbContext(connection.Options);
            ExchangeRateToUsdCurrentRepository exchangeRateToUsdCurrentRepository =
                new ExchangeRateToUsdCurrentRepository(spaceDbContext);
            ExchangeAccountHistoryRepository exchangeAccountHistoryRepository =
                new ExchangeAccountHistoryRepository(spaceDbContext);
            ExchangeRateToUsdHistoryRepository exchangeRateToUsdHistoryRepository =
                new ExchangeRateToUsdHistoryRepository(spaceDbContext);

            IBankAccountRepository bankAccountRepository = new BankAccountRepository(spaceDbContext);
            IUserRepository userRepository = new UserRepository(spaceDbContext, bankAccountRepository);
            IHttpContextAccessor contextAccessor = new HttpContextAccessor();
            IMapper mapper = null;

            UserService userService = new UserService(userRepository, contextAccessor);

            ICurrencyService currencyService =
                new CurrencyService(userService, exchangeRateToUsdCurrentRepository, exchangeAccountHistoryRepository,
                    exchangeRateToUsdHistoryRepository, mapper);

            var exchangeRates = new GottenCurrency();
            var currentDate = DateTime.Now;
            while (true)
            {
                if ((currentDate.Minute % 3) == 0)
                {
                    currencyService.MoveCurrentExchangesDbToHistoryDb(exchangeRateToUsdCurrentRepository, exchangeRateToUsdHistoryRepository);
                    currencyService.DeleteCurrentExchRatesFromDb(exchangeRateToUsdCurrentRepository);
                    exchangeRates = currencyService.GetExchangeRates();
                    currencyService.PutCurrentExchangeRatesToDb(exchangeRateToUsdCurrentRepository, exchangeRates);
                    Thread.Sleep(60000);
                }
                currentDate = DateTime.Now;
            }
        }
    }
}