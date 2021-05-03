using Microsoft.EntityFrameworkCore;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private IBankAccountRepository _bankAccountRepository;

        public UserRepository(SpaceDbContext spaceDbContext,
            IBankAccountRepository bankAccountRepository)
            : base(spaceDbContext)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public User Get(string name)
        {
            return _dbSet.SingleOrDefault(x =>
                x.Name.ToLower() == name.ToLower()
                || x.Login.ToLower() == name.ToLower());
        }

        public override void Remove(User user)
        {
            var admin = Get(SeedExtension.AdminName);

            if (user.Id == admin.Id)
            {
                throw new Exception("Never remove admin");
            }

            var copy = user.BankAccounts.ToList();
            foreach (var bankAccount in copy)
            {
                bankAccount.Owner = admin;
                _bankAccountRepository.Save(bankAccount);
            }

            base.Remove(user);
        }
    }
}
