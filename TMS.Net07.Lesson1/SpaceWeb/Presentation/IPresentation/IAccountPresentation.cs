using SpaceWeb.Models;
using System.Collections.Generic;

namespace SpaceWeb.Presentation
{
    public interface IAccountPresentation
    {
        BankAccountViewModel GetViewModelForIndex(long id);
        public List<BankAccountViewModel> GetAllViewModelsForCreation();
    }
}