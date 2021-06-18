using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public interface IProfileRepository : IBaseRepository<Questionary>
    {
        void ChangeProfile(Questionary model, string userID);
    }
}