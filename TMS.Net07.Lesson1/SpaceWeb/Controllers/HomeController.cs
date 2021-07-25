using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpaceWeb.Controllers.CustomAttribute;
using SpaceWeb.Models.Chart;
using SpaceWeb.Service;
using System.IO;
using System.Linq;
using System.Text.Json;
using MazeCore;
using AutoMapper;
using SpaceWeb.Models.Maze;
using MazeCore.GraphStuff;

namespace SpaceWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserService _userService;
        private MazeBuilder _mazeBuilder;
        private IMapper _mapper;
        private IPathHelper _pathHelper;

        public HomeController(ILogger<HomeController> logger,
            IUserService userService,
            MazeBuilder mazeBuilder,
            IMapper mapper,
            IPathHelper pathHelper)
        {
            _logger = logger;
            _userService = userService;
            _mazeBuilder = mazeBuilder;
            _mapper = mapper;
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

            if (user != null)
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
        
        public IActionResult Maze()
        {
            var mazeLevel = _mazeBuilder.Build(4, 4, seed: 50);
            var viewModel = _mapper.Map<MazeViewModel>(mazeLevel);
            return View(viewModel);
        }

        public IActionResult TheLongestWay(int x, int y)
        {
            var mazeLevel = _mazeBuilder.Build(4, 4, seed: 50);

            var graph = _mazeBuilder.BuildGraph(mazeLevel);

            var ver = graph.Vertices
                .Single(ver => ver.BaseCell.X == x && ver.BaseCell.Y == y);
            ver.DistanceFromRoot = 0;
            graph.SetDistanceFromRoot(ver);

            var max = graph.Vertices.Max(x => x.DistanceFromRoot);

            return Json(max);
        }
        public IActionResult TheRichestWay(int x, int y)
        {
            var mazeLevel = _mazeBuilder.Build(4, 4, seed: 50);

            var graph = _mazeBuilder.BuildGraph(mazeLevel);

            var ver = graph.Vertices
                .Single(ver => ver.BaseCell.X == x && ver.BaseCell.Y == y);
            var s = graph.GetRichestWay(ver);

            return Json(s);
        }

        public IActionResult PossibleWays(int x, int y)
        {
            var mazeLevel = _mazeBuilder.Build(4, 4, seed: 50);

            var graph = _mazeBuilder.BuildGraph(mazeLevel);

            var root = graph.Vertices.Single(ver => ver.BaseCell.X == x && ver.BaseCell.Y == y);

            var ways = graph.GetAllWays(root);

            var viewModels = ways.Select(x => _mapper.Map<WayViewModel>(x)).ToList();

            return Json(viewModels);
        }
    }
}
