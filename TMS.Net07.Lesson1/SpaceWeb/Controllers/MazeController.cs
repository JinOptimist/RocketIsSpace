using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MazeCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Novacode;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.Models.Maze;
using SpaceWeb.Service;
using MazeLevel = SpaceWeb.EfStuff.Model.MazeLevel;

namespace SpaceWeb.Controllers
{
    public class MazeController : Controller
    {
        private UserService _userService;
        private MazeBuilder _mazeBuilder;
        private MazeLevelRepository _mazeLevelRepository;
        private IMapper _mapper;

        public MazeController(UserService userService,
            MazeBuilder mazeBuilder,
            IMapper mapper)
        {
            _userService = userService;
            _mazeBuilder = mazeBuilder;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var models = new List<MazeViewModel>();
            var currentUser = _userService.GetCurrent();

            if (currentUser == null)
            {
                var mazeLevel = _mazeBuilder.Build(4, 4, seed: 50);
                var viewModel = _mapper.Map<MazeViewModel>(mazeLevel);
                models.Add(viewModel);
            }

            var mazeLevels = _mazeLevelRepository
                .GetAll()
                .Where(x => x.User.Id == currentUser.Id);
            models.AddRange(mazeLevels.Select(level => _mapper.Map<MazeViewModel>(level)));

            return View(models);
        }

        [Authorize]
        public IActionResult CreateNewMaze()
        {
            var newMaze = _mazeBuilder.Build(4, 4, seed: 50);
            var mazeForDb = _mapper.Map<MazeCore.MazeLevel, MazeLevel>(newMaze);
            _mazeLevelRepository.Save(mazeForDb);

            return RedirectToAction("Index");
        }

        public IActionResult TheLongestWay(int x, int y)
        {
            var mazeLevel = _mazeBuilder.Build(4, 4, seed: 50);

            var graph = _mazeBuilder.BuildGraph(mazeLevel);

            var ver = graph.Vertices
                .Single(ver => ver.BaseCell.X == x && ver.BaseCell.Y == y);
            ver.DistanceFromRoot = 0;
            graph.SetDistanceFromRoot(ver);

            var max = graph.Vertices.Max(cell => cell.DistanceFromRoot);

            return Json(max);
        }
    }
}
