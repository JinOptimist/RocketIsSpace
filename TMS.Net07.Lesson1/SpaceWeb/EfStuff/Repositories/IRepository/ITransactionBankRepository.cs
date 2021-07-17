﻿using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TransactionBank = SpaceWeb.EfStuff.Model.TransactionBank;

namespace SpaceWeb.EfStuff.Repositories.IRepository
{
    public interface ITransactionBankRepository : IBaseRepository<TransactionBank>
    {
        public decimal GetBankCardFrom(string AccountNumber);
        public decimal GetBankCardTo(string AccountNumber);

    }
}
