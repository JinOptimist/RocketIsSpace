using SpaceWeb.Models;
using System.Collections.Generic;

namespace SpaceWeb.Presentation
{
    public interface IRelicPresentation
    {
        List<RelicViewModel> GetIndexViewModels();
    }
}