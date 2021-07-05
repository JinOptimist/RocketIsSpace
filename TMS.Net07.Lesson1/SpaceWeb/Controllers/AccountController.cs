using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Novacode;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.EfStuff.Repositories;
using SpaceWeb.EfStuff.Repositories.IRepository;
using SpaceWeb.Models;
using SpaceWeb.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IBankAccountRepository _bankAccountRepository;
        private IMapper _mapper;
        private IWebHostEnvironment _hostEnvironment;
        private UserService _userService;

        public AccountController(IBankAccountRepository bankAccountRepository,
            QuestionaryRepository profileRepository,
            IUserRepository userRepository,
            IMapper mapper, UserService userService,
            IWebHostEnvironment hostEnvironment)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
            _userService = userService;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index(long id)
        {
            if (id > 0)
            {
                var user = _userService.GetCurrent();
                var dbModel = user.BankAccounts.SingleOrDefault(x => x.Id == id);
                var viewModel = _mapper.Map<BankAccountViewModel>(dbModel);

                var index = user.BankAccounts.IndexOf(dbModel);

                viewModel.AccountIndex = index;
                //для того, чтобы вставить индекс активного аккаунта при загрузке страницы

                return View(viewModel);
            }
            return RedirectToAction("Creation");
        }

        public IActionResult Remove(long id)
        {
            _bankAccountRepository.Remove(id);

            var user = _userService.GetCurrent();

            var newId = user.BankAccounts?.FirstOrDefault()?.Id;
            if (newId != null)
            {
                //return RedirectToAction("Index",  new { id = (long)newId });
                return Redirect($"/Account/Index?id={newId}");
            }
            return RedirectToAction("Creation");
        }

        [HttpGet]
        public IActionResult Creation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Creation(BankAccountViewModel viewModel)
        {
            int accountLifeTime;

            var type = viewModel.Amount.GetType();

            if (viewModel.Currency == Currency.BYN) //заменить двойной if
            {
                if (viewModel.Name == null)
                {
                    viewModel.Name = "Счет";
                }
                accountLifeTime = 5;
            }
            else
            {
                if (viewModel.Name == null)
                {
                    viewModel.Name = "Валютный счет";
                }
                accountLifeTime = 3;
            }

            StringBuilder sb = new StringBuilder();

            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                sb.Append(rnd.Next(0, 9));
            }
            viewModel.AccountNumber = sb.ToString();

            viewModel.CreationDate = DateTime.Now;

            viewModel.ExpireDate = viewModel.CreationDate.AddYears(accountLifeTime);

            var modelDB =
                _mapper.Map<BankAccount>(viewModel);

            var user = _userService.GetCurrent();

            modelDB.Owner = user;

            _bankAccountRepository.Save(modelDB);

            var id = user.BankAccounts?.
                SingleOrDefault(x => x.AccountNumber == viewModel.AccountNumber)
                .Id;

            //return RedirectToAction("Index", new { id });

            return RedirectToRoute("default", new { controller = "Account", action = "Index", id });
        }

        public IActionResult DownloadAccountsInfo()
        {
            var webPath = _hostEnvironment.WebRootPath;
            var user = _userService.GetCurrent();
            var path = Path.Combine(webPath, "TempFile", $"{user.Id}.docx");
            var pathIntroImage = Path.Combine(webPath, "image/bank/bank_cards.png");
            var pathLineImage = Path.Combine(webPath, "image/separatingLine.png");
            var countRows = 6;
            var accounts = _bankAccountRepository.GetAll().Where(x => x.Owner.Id == user.Id).ToList();
            var accountsNumber = 1;
            var colorNow = 0;
            Border slimLine = new Border(BorderStyle.Tcbs_single, BorderSize.one, 0, Color.Black);
            Border boldLine = new Border(BorderStyle.Tcbs_single, BorderSize.seven, 0, Color.Black);
            List<Color> colorList = new List<Color>() {
                Color.OrangeRed,
                Color.CadetBlue,
                Color.LightGreen,
                Color.PeachPuff,
                Color.Aqua,
                Color.RosyBrown
            };

            using (var doc = DocX.Create(path))
            {
                var picIntro = doc.AddImage(pathIntroImage, "image/png").CreatePicture();
                picIntro.Width = 600;
                picIntro.Height = 150;
                doc.InsertParagraph().InsertPicture(picIntro);

                doc.InsertParagraph($"Детали всех счетов (всего: {accounts.Count})")
                    .Font("Comic Sans MS")
                    .Bold()
                    .FontSize(25)
                    .Alignment = Alignment.center;
                doc.InsertParagraph("");

                var picLine = doc.AddImage(pathLineImage, "image/png").CreatePicture();
                picLine.Width = 600;
                picLine.Height = 50;
                doc.InsertParagraph().InsertPicture(picLine);
                doc.InsertParagraph("");

                foreach (var account in accounts)
                {
                    doc.InsertParagraph().Append($"Счет №{accountsNumber++} - {account.Name}").Bold().FontSize(16).Italic().Alignment = Alignment.center;

                    var table = doc.InsertTable(countRows, 2);

                    table.Rows[0].Cells[0].Paragraphs.First().Append("Account name").Bold().FontSize(14).Italic();
                    table.Rows[1].Cells[0].Paragraphs.First().Append("Currency").Bold().FontSize(14).Italic();
                    table.Rows[2].Cells[0].Paragraphs.First().Append("Amount").Bold().FontSize(14).Italic();
                    table.Rows[3].Cells[0].Paragraphs.First().Append("Account number").Bold().FontSize(14).Italic();
                    table.Rows[4].Cells[0].Paragraphs.First().Append("Creation date").Bold().FontSize(14).Italic();
                    table.Rows[5].Cells[0].Paragraphs.First().Append("Expiry date").Bold().FontSize(14).Italic();


                    for (int i = 0; i < countRows; i++)
                    {
                        table.Rows[i].Cells[0].FillColor = colorList[colorNow];
                    }
                    if (++colorNow == colorList.Count())
                    {
                        colorNow = 0;
                    }

                    table.Rows[0].Cells[1].Paragraphs.First().Append(account.Name).FontSize(12).Alignment = Alignment.center;
                    table.Rows[1].Cells[1].Paragraphs.First().Append(account.Currency.ToString()).FontSize(12).Alignment = Alignment.center;
                    table.Rows[2].Cells[1].Paragraphs.First().Append(account.Amount.ToString()).FontSize(12).Alignment = Alignment.center;
                    table.Rows[3].Cells[1].Paragraphs.First().Append(account.AccountNumber).FontSize(12).Alignment = Alignment.center;
                    table.Rows[4].Cells[1].Paragraphs.First().Append(account.CreationDate.ToString()).FontSize(12).Alignment = Alignment.center;
                    table.Rows[5].Cells[1].Paragraphs.First().Append(account.ExpireDate.ToString()).FontSize(12).Alignment = Alignment.center;

                    table.SetBorder(TableBorderType.InsideH, slimLine);
                    table.SetBorder(TableBorderType.InsideV, slimLine);
                    table.SetBorder(TableBorderType.Bottom, boldLine);
                    table.SetBorder(TableBorderType.Top, boldLine);
                    table.SetBorder(TableBorderType.Left, boldLine);
                    table.SetBorder(TableBorderType.Right, boldLine);

                    table.Alignment = Alignment.center;
                    doc.InsertParagraph();
                }

                doc.Save();
            }

            var contentTypeDocx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            var fileName = "Info about accounts.docx";
            return PhysicalFile(path, contentTypeDocx, fileName);
        }

        public IActionResult DownloadLog(long id)
        {
            var webPath = _hostEnvironment.WebRootPath;
            var user = _userService.GetCurrent();
            var path = Path.Combine(webPath, "TempFile", $"{user.Id}.docx");
            var account = _bankAccountRepository.Get(id);
            var countRows = 6;
            Border slimLine = new Border(BorderStyle.Tcbs_single, BorderSize.one, 0, Color.Black);
            Border boldLine = new Border(BorderStyle.Tcbs_single, BorderSize.seven, 0, Color.Black);

            using (var doc = DocX.Create(path))
            {
                doc.InsertParagraph($"Детали счета \"{account.Name}\":")
                    .Font("Comic Sans MS")
                    .Bold()
                    .FontSize(25)
                    .Alignment = Alignment.center;
                doc.InsertParagraph("");

                var table = doc.InsertTable(countRows, 2);

                table.Rows[0].Cells[0].Paragraphs.First().Append("Account name").Bold().FontSize(14).Italic();
                table.Rows[1].Cells[0].Paragraphs.First().Append("Currency").Bold().FontSize(14).Italic();
                table.Rows[2].Cells[0].Paragraphs.First().Append("Amount").Bold().FontSize(14).Italic();
                table.Rows[3].Cells[0].Paragraphs.First().Append("Account number").Bold().FontSize(14).Italic();
                table.Rows[4].Cells[0].Paragraphs.First().Append("Creation date").Bold().FontSize(14).Italic();
                table.Rows[5].Cells[0].Paragraphs.First().Append("Expiry date").Bold().FontSize(14).Italic();

                table.Rows[0].Cells[1].Paragraphs.First().Append(account.Name).FontSize(12).Alignment = Alignment.center;
                table.Rows[1].Cells[1].Paragraphs.First().Append(account.Currency.ToString()).FontSize(12).Alignment = Alignment.center;
                table.Rows[2].Cells[1].Paragraphs.First().Append(account.Amount.ToString()).FontSize(12).Alignment = Alignment.center;
                table.Rows[3].Cells[1].Paragraphs.First().Append(account.AccountNumber).FontSize(12).Alignment = Alignment.center;
                table.Rows[4].Cells[1].Paragraphs.First().Append(account.CreationDate.ToString()).FontSize(12).Alignment = Alignment.center;
                table.Rows[5].Cells[1].Paragraphs.First().Append(account.ExpireDate.ToString()).FontSize(12).Alignment = Alignment.center;

                for (int i = 0; i < countRows; i++)
                {
                    table.Rows[i].Cells[0].FillColor = Color.LightGreen;
                }

                table.SetBorder(TableBorderType.InsideH, slimLine);
                table.SetBorder(TableBorderType.InsideV, slimLine);
                table.SetBorder(TableBorderType.Bottom, boldLine);
                table.SetBorder(TableBorderType.Top, boldLine);
                table.SetBorder(TableBorderType.Left, boldLine);
                table.SetBorder(TableBorderType.Right, boldLine);

                table.Alignment = Alignment.center;

                doc.Save();
            }

            var contentTypeDocx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            var fileName = $"Info about '{account.Name}' account.docx";
            return PhysicalFile(path, contentTypeDocx, fileName);
        }
        public IActionResult UpdateAmount(string accoutNumber, int delta)
        {
            var account = _bankAccountRepository.Get(accoutNumber);
            account.Amount += delta;
            _bankAccountRepository.Save(account);
            return Json(true);
        }

        public IActionResult GetByName(string name)
        {
            var userId = _userService.GetCurrent().Id;
            var answer = _bankAccountRepository.GetByName(userId, name).Select(x => x.Id);
            return Json(answer);
        }
    }
}
