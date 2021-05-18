using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories.IRepository
{
    public interface IBanksCardRepository : IBaseRepository<BanksCard>
    {
        List<BanksCard> Get(string CardNumber);

        List<BanksCard> GetCardUser(long userId);

        decimal GetAmount(string AccountNumber);

        public string GetTransaction(long transferToId);

    }
}
