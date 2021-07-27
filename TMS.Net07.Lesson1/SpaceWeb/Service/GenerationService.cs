using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWeb.Service
{
    public class GenerationService : IGenerationService
    {
        private ITransactionBankRepository _transactionBankRepository;

        public GenerationService(ITransactionBankRepository transactionBankRepository)
        {
            _transactionBankRepository = transactionBankRepository;
        }
        public string GenerateAccountNumber(int accountNumberLength = 10)
        {
            StringBuilder sb = new StringBuilder();

            Random rnd = new Random();

            for (int i = 0; i < accountNumberLength; i++)
            {
                sb.Append(rnd.Next(0, 9));
            }

            return sb.ToString();
        }

        public string GenerateTransactionNumber()
        {
            StringBuilder sb = new StringBuilder();

            var transactionsCount = _transactionBankRepository.GetAll().Count();

            var digits = transactionsCount.ToString().Length;

            for (int i = digits; i < 6; i++)
            {
                sb.Append(0);
            }

            sb.Append(transactionsCount + 1);

            return sb.ToString();
        }
    }
}
