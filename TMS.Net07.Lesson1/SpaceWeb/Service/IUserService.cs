using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.Service
{
    public interface IUserService
    {
        User GetCurrent();
        bool IsAdmin();
        bool IsEngineer();
    }
}