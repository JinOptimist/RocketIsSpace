using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceWeb.EfStuff.Repositories;
using AutoMapper;

namespace SpaceWeb.Service
{
    public interface ITransactionService
    {
        public bool TransferFunds(int fromAccountId, int toAccountId, decimal transferAmount);
    }
}
