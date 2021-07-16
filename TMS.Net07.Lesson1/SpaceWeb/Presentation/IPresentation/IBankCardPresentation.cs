using SpaceWeb.Models;
using System.Collections.Generic;

namespace SpaceWeb.Presentation
{
    public interface IBankCardPresentation 
    {
        List<BanksCardViewModel> GetIndexViewModels();
    }
}