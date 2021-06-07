using SpaceWeb.Models;

namespace SpaceWeb.Presentation
{
    public interface IBankPresentation
    {
        QuestionaryViewModel GetProfileViewModel(long id);
    }
}