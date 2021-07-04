using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SpaceWeb.Service
{
    public class PathHelper : IPathHelper
    {
        public const string UrlFolder = "/image/avatars/";
        private IWebHostEnvironment _hostEnvironment;

        public PathHelper(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public string GetPathToAvatarFolder()
        {
            var webPath = _hostEnvironment.WebRootPath;
            var path = Path.Combine(webPath, "image", "avatars");
            if (Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public string GetPathToAvatarByUser(long userId)
        {
            var avatarFolderPath = GetPathToAvatarFolder();
            return Path.Combine(avatarFolderPath, $"{userId}.jpg");
        }

        public string GetAvatarUrlByUser(long userId)
        {
            return $"{UrlFolder}{userId}.jpg";
        }

        public string GetAvatarUrlByFileName(string file)
        {
            return $"{UrlFolder}{file}";
        }
    }
}
