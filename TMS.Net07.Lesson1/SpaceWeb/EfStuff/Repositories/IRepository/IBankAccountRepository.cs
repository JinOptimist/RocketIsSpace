﻿using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories.IRepository
{
    public interface IBankAccountRepository : IBaseRepository<BankAccount>
    {
        BankAccount Get(string AccountNumber);

        List<BankAccount> GetBankAccounts(long userId);

        List<Currency> GetCurrencies(long userId);
    }
}
