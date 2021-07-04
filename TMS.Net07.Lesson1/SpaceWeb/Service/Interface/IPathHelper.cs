namespace SpaceWeb.Service
{
    public interface IPathHelper
    {
        string GetAvatarUrlByUser(long userId);
        string GetPathToAvatarByUser(long userId);
        string GetPathToAvatarFolder();
        string GetAvatarUrlByFileName(string file);
    }
}