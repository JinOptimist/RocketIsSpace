﻿using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class BankAccountRepository : BaseRepository<BankAccount>
    {
        public BankAccountRepository(SpaceDbContext spaceDbContext) :
            base(spaceDbContext)
        {
        }

        public BankAccount Get(string AccountNumber)
        {
            return _dbSet.SingleOrDefault(x => x.AccountNumber == AccountNumber);
        }
    }
}
