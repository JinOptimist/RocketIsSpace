using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public interface IProfileRepository : IBaseRepository<Profile>
    {
        void ChangeProfile(Profile model, string userID);
    }
}