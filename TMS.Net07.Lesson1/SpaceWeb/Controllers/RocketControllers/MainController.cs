using Microsoft.AspNetCore.Mvc;

namespace SpaceWeb.Controllers.RocketControllers
{
    public class MainController : Controller
    {
        // GET
        public IActionResult MainPage()
        {
            return View();
        }
    }
}