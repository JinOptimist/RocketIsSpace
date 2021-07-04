using System.Security.Claims;
using SpaceWeb.EfStuff.Model;

namespace SpaceWeb.Service
{
    public interface IUserService
    {
        User GetCurrent();
        string GetAvatarUrl();
        string GetAvatarUrl(string userAvatar);
        bool IsEngineer();
        bool IsChiefBankEmployee();
        bool IsBankEmployeeOrHigher();
        bool IsBankClientOrHigher();
        bool IsAdmin();
        ClaimsPrincipal GetPrincipal(User user);
        bool IsLeader();
    }
}