using SpaceWeb.Models;
using System.Collections.Generic;

namespace SpaceWeb.Presentation
{
    public interface IAccountPresentation
    {
        List<BankAccountViewModel> GetAllViewModelsForCreation();
        string GetJsonForRemove(long id, string password);
        BankAccountViewModel GetViewModelForIndex(long id);
        public long GetCreatedAccountId(BankAccountViewModel viewModel);
        public bool AccountFreezeResult(long id);
        public string UpdateAmountResult(long id, decimal amount);
        public string GetJsonAsTransferResult(long fromAccountId, string toAccountNumber, decimal transferAmount);
    }
}