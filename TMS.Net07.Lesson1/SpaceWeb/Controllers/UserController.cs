using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpaceWeb.EfStuff;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Models.Human;
using SpaceWeb.Models.RocketModels;
using SpaceWeb.Service;
using System;
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
        private IBankAccountRepository _bankAccountRepository;
        private IMapper _mapper;
        private UserService _userService;
        private ICurrencyService _currencyService;
        private IWebHostEnvironment _hostEnvironment;
        private ILogger<UserController> _logger;

        public static int Counter = 0;

        public UserController(IUserRepository userRepository, IMapper mapper,
            UserService userService, ICurrencyService currencyService, IWebHostEnvironment hostEnvironment,
            IBankAccountRepository bankAccountRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
            _currencyService = currencyService;
            _hostEnvironment = hostEnvironment;
            _bankAccountRepository = bankAccountRepository;
            _logger = logger;
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
            viewModel.DefaultCurrency = user.DefaultCurrency;
            viewModel.MyCurrencies = _bankAccountRepository.GetCurrencies(user.Id);

            decimal allAmountInUsd = 0;
            var accounts = _bankAccountRepository.GetBankAccounts(user.Id);

            foreach (var account in accounts)
            {
                var amount = _currencyService.ConvertByAlex(account.Currency, account.Amount, Currency.USD);
                allAmountInUsd += amount;
            }

            decimal amountAllMoneyInDefaultCurrency = 0;

            if (user.DefaultCurrency != 0)
            {
                amountAllMoneyInDefaultCurrency = _currencyService.ConvertByAlex(Currency.USD, allAmountInUsd, user.DefaultCurrency);
            }
            viewModel.AmountAllMoneyInDefaultCurrency = Math.Round(amountAllMoneyInDefaultCurrency, 2);

            return View(viewModel);
        }

        public IActionResult UpdateFavCurrency(Currency currency)
        {
            var user = _userService.GetCurrent();
            user.DefaultCurrency = currency;
            _userRepository.Save(user);

            return Json(true);
        }

        public IActionResult UpdateLang(Lang lang)
        {
            var user = _userService.GetCurrent();
            user.Lang = lang;
            _userRepository.Save(user);

            return Json(true);
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

                _logger.LogInformation($"User {user.Id} change avatar");
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

            if (user == null || user.Password != model.Password)
            {
                return View(model);
            }

            await HttpContext.SignInAsync(
                _userService.GetPrincipal(user));

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
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isUserUniq = _userRepository.Get(model.Login) == null;
            if (isUserUniq)
            {
                var user = _mapper.Map<User>(model);
                _userRepository.Save(user);


                await HttpContext.SignInAsync(
                    _userService.GetPrincipal(user));

                return RedirectToAction("Profile", "User");
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

            if ((model.Password == GlobalConst.TELEGRAMGROUPPASS)
                && (model.Link.ToLower() == nameof(GlobalConst.TELEGRAMGROUPLINK).ToLower()))
            {
                return Redirect(GlobalConst.TELEGRAMGROUPLINK);
            }
            else if ((model.Password == GlobalConst.YOUTUBETEACHERPASS)
                && (model.Link.ToLower() == nameof(GlobalConst.YOUTUBETEACHERLINK).ToLower()))
            {
                return Redirect(GlobalConst.YOUTUBETEACHERLINK);
            }
            else
            {
                return RedirectToAction("SocialsWrongPass", "User");
            }
        }

        [HttpGet]
        public IActionResult SocialsWrongPass()
        {
            return View();
        }

        public IActionResult EmployeeProfile()
        {
            var user = _userService.GetCurrent();
            var viewModel = _mapper.Map<EmployeeProfileViewModel>(user);
            return View(viewModel);
        }
    }
}