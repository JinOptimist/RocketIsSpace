using SpaceWeb.Models;
using System.Collections.Generic;

namespace SpaceWeb.Presentation
{
    public interface IBankAccountPresentation
    {
        List<BankAccountViewModel> GetViewModelForCabinet(BankAccountViewModel viewModel);
    }
}