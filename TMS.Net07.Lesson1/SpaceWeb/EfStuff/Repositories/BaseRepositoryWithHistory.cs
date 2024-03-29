﻿using AutoMapper;
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
    public abstract class BaseRepositoryWithHistory<ModelType, ModelHistoryType> : BaseRepository<ModelType>
        where ModelType : BaseModel
        where ModelHistoryType : BaseHistoryModel
    {
        protected DbSet<ModelHistoryType> _historyDbSet;
        private IMapper _mapper;
        private IHttpContextAccessor _contextAccessor;
        private ITransactionBankRepository _transactionBankRepository;

        public BaseRepositoryWithHistory(SpaceDbContext spaceDbContext, 
            IMapper mapper, 
            IHttpContextAccessor contextAccessor,
            ITransactionBankRepository transactionBankRepository) : base(spaceDbContext)
        {
            _spaceDbContext = spaceDbContext;
            _historyDbSet = _spaceDbContext.Set<ModelHistoryType>();
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _transactionBankRepository = transactionBankRepository;
        }

        public override void Save(ModelType model)
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

        public override void Remove(ModelType model)
        {

            SaveHistory(model, "Delete");

            if (model is BankAccount)
            {
                var account = _spaceDbContext.BankAccount.Single(x => x.Id == model.Id);

                account.IncomingTransactions.ForEach( x => {
                    _spaceDbContext.Remove(x);
                });

                account.OutcomingTransactions.ForEach(x => {
                    _spaceDbContext.Remove(x);
                });

                _spaceDbContext.Update(account);
            }

            base.Remove(model);
            
            _spaceDbContext.SaveChanges();
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

        private void SaveHistory(ModelType model, string action)
        {
            var historyModel = _mapper.Map<ModelHistoryType>(model);
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