using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories
{
    public class BankAccountHistoryRepository : BaseRepository<BankAccountHistory>
    {
        protected DbSet<BankAccountHistory> _historyDbSet;
        private IMapper _mapper;
        private IHttpContextAccessor _contextAccessor;

        public BankAccountHistoryRepository(SpaceDbContext spaceDbContext, 
            IMapper mapper, 
            IHttpContextAccessor contextAccessor) : base(spaceDbContext)
        {
            _spaceDbContext = spaceDbContext;
            _historyDbSet = _spaceDbContext.Set<BankAccountHistory>();
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public override void Save(BankAccountHistory model)
        {
            bool isNew = model.Id == 0;

            if (isNew)
            {
                _dbSet.Add(model);
                SaveHistory(model, "Add");
            }
            else
            {
                _dbSet.Update(model);
                SaveHistory(model, "Update");
            }

            _spaceDbContext.SaveChanges();
        }

        public BankAccountHistory GetLastAccountHistoryModel(BankAccount account)
        {
            return _dbSet.LastOrDefault(x => x.AccountNumber == account.AccountNumber);
        }

        public override void Remove(long id)
        {
            var model = Get(id);
            Remove(model);
        }

        public override void Remove(IEnumerable<long> ids)
        {
            foreach (var userid in ids)
            {
                Remove(userid);
            }
        }

        public void SaveHistory(BankAccount model, string action)
        {
            var historyModel = _mapper.Map<BankAccountHistory>(model);
            historyModel.Id = default;

            historyModel.DateOfChange = DateTime.Now;

            var currentUser = GetCurrent();
            historyModel.UserWhoChanged = currentUser;

            historyModel.Action = action;

            _historyDbSet.Add(historyModel);
        }

        private User GetCurrent()
        {
            var idStr = _contextAccessor.HttpContext?.User
                ?.Claims.SingleOrDefault(x => x.Type == "Id")?.Value;
            if (string.IsNullOrEmpty(idStr))
            {
                return null;
            }

            var id = long.Parse(idStr);

            var userDbSet = _spaceDbContext.Set<User>();
            return userDbSet.SingleOrDefault(x => x.Id == id);
        }
    }
}