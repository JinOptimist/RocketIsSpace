using SpaceWeb.Models;

namespace SpaceWeb.Presentation
{
    public interface IBankPresentation
    {
        UserProfileViewModel GetProfileViewModel(long id);
    }
}