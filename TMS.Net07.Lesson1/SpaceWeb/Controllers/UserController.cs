using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Models.RocketModels;
using SpaceWeb.Service;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private UserService _userService;
        private IWebHostEnvironment _hostEnvironment;

        public static int Counter = 0;

        public UserController(IUserRepository userRepository, IMapper mapper,
            UserService userService, IWebHostEnvironment hostEnvironment)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
            _hostEnvironment = hostEnvironment;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            var user = _userService.GetCurrent();
            //user.BankAccounts;
            var viewModel = _mapper.Map<ProfileViewModel>(user);
            var bankViewModels = user
                .BankAccounts
                .Select(x => _mapper.Map<BankAccountViewModel>(x)).ToList();
            viewModel.MyAccounts = bankViewModels;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(ProfileUpdateViewModel viewModel)
        {
            var user = _userService.GetCurrent();

            if (viewModel.Avatar != null)
            {
                var webPath = _hostEnvironment.WebRootPath;
                var path = Path.Combine(webPath, "image", "avatars", $"{user.Id}.jpg");
                using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    await viewModel.Avatar.CopyToAsync(fileStream);
                }
                user.AvatarUrl = $"/image/avatars/{user.Id}.jpg";
            }

            user.Email = viewModel.Email;
            _userRepository.Save(user);

            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new RegistrationViewModel();
            var returnUrl = Request.Query["ReturnUrl"];
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userRepository.Get(model.Login);

            if (user == null)
            {
                ModelState.AddModelError(
                    nameof(RegistrationViewModel.Login),
                    "Нет такого пользователя");
                return View(model);
            }

            if (user.Password != model.Password)
            {
                ModelState.AddModelError(
                    nameof(RegistrationViewModel.Password),
                    "Не правильный праоль");
                return View(model);
            }

            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(
                ClaimTypes.AuthenticationMethod,
                Startup.AuthMethod));
            var claimsIdentity = new ClaimsIdentity(claims, Startup.AuthMethod);
            var principal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(principal);

            if (!string.IsNullOrEmpty(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            var model = new RegistrationViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Старый способ.
            //var isUserUniq = true;
            //foreach (var user in Users)
            //{
            //    if (user.UserName == model.Login)
            //    {
            //        isUserUniq = false;
            //    }
            //}

            //Новый способ LINQ
            var isUserUniq = _userRepository.Get(model.Login) == null;
            if (isUserUniq)
            {
                var user = new User()
                {
                    Name = model.Login,
                    Password = model.Password,
                    Age = 18
                };
                _userRepository.Save(user);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword(long id)
        {
            var viewModel = new ChangePasswordViewModel() { Id = id };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel viewModel)
        {
            var user = _userRepository.Get(viewModel.Id);
            if (user.Password != viewModel.OldPassword)
            {
                ModelState.AddModelError(nameof(ChangePasswordViewModel.OldPassword),
                    "Не правильный старый пароль");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            user.Password = viewModel.NewPassword;
            _userRepository.Save(user);
            return View(viewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public JsonResult IsUserExist(string name)
        {
            Thread.Sleep(3000);
            var isExistUserWithTheName =
                _userRepository.Get(name) != null;
            return Json(isExistUserWithTheName);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangeName()
        {
            var user = _userService.GetCurrent();
            var model = new ChangeNameViewModel()
            {
                Id = user.Id,
                OldName = user.Name
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangeName(ChangeNameViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var user = _userService.GetCurrent();
            user.Name = viewModel.NewName;
            _userRepository.Save(user);
            return RedirectToAction("Profile", "User");
        }

        [HttpPost]
        public IActionResult Socials(SocialsPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if ((model.Password == SocialsPassword.TgAllGroup.ToString()) 
                && (model.Link == nameof(SocialsPassword.TgAllGroup)))
            {
                return Redirect("https://t.me/joinchat/Tv44VQeM8nXUusnV");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }
    }
}