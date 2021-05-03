using SpaceWeb.Models.Human;
using System.Collections.Generic;

namespace SpaceWeb.Presentation
{
    public interface IHumanPresentation
    {
        List<ShortUserViewModel> GetViewModelForAllUsers();

        void Remove(List<long> userIds);
    }
}