using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpaceWeb.Controllers.CustomAttribute;
using SpaceWeb.Models.Chart;
using SpaceWeb.Service;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SpaceWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserService _userService;
        private IPathHelper _pathHelper;

        public HomeController(ILogger<HomeController> logger, UserService userService, 
            IPathHelper pathHelper)
        {
            _logger = logger;
            _userService = userService;
            _pathHelper = pathHelper;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Smile");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccountChartInfo()
        {
            var user = _userService?.GetCurrent();

            //проект не грузится, если юзер не залогинен

            if (user!=null)
            {
                var currencies = user.BankAccounts.Select(x => x.Currency).Distinct();

                var chartViewModel = new ChartViewModel();
                chartViewModel.Labels = currencies.Select(x => x.ToString()).ToList();
                var datasetViewModel = new DatasetViewModel()
                {
                    Label = "Валюты"
                };
                datasetViewModel.Data =
                    currencies.Select(c =>
                        user.BankAccounts
                            .Where(b => b.Currency == c)
                            .Select(b => b.Amount)
                            .Sum())
                    .ToList();

                chartViewModel.Datasets.Add(datasetViewModel);

                return Json(chartViewModel);
            }
            return Json("null user");
        }

        public IActionResult ImageForCarousel()
        {
            var carouselFolderPath = _pathHelper.GetPathToCarouselFolder();
            var filesPath = Directory.GetFiles(carouselFolderPath);
            var img = filesPath
                .Where(filePath => Path.GetExtension(filePath) == ".jpg")
                .ToList();

            return Json(img);
        }
    }
}
