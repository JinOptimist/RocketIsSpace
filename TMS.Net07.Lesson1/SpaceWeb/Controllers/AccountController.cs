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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SpaceWeb.Presentation;
using Newtonsoft.Json;


namespace SpaceWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IBankAccountRepository _bankAccountRepository;
        private IMapper _mapper;
        private IWebHostEnvironment _hostEnvironment;
        private IUserService _userService;
        private IAccountPresentation _accountPresentation;
        private ITransactionService _transactionService;

        public AccountController(IBankAccountRepository bankAccountRepository,
            IMapper mapper, 
            IUserService userService,
            IWebHostEnvironment hostEnvironment,
            IAccountPresentation accountPresentation,
            ITransactionService transactionService)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _userService = userService;
            _accountPresentation = accountPresentation;
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult Index(long id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Creation");
            }

            var user = _userService.GetCurrent();

            if (!user.BankAccounts.Any(x => x.Id == id))
            {
                return RedirectToAction("Creation");
            }

            var viewModel = _accountPresentation.GetViewModelForIndex(id);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Remove(long id, string password)
        {
            var response = _accountPresentation.GetJsonForRemove(id, password);

            return Json(JsonConvert.DeserializeObject(response));
        }

        [HttpGet]
        public IActionResult Creation()
        {
            var viewModel = _accountPresentation.GetAllViewModelsForCreation();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Creation(BankAccountViewModel viewModel)
        {
            var id = _accountPresentation.GetCreatedAccountId(viewModel);

            return RedirectToAction("Index", new { id });
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
            var folderPath = Path.Combine(webPath, "TempFile");
            var user = _userService.GetCurrent();
            var path = Path.Combine(webPath, "TempFile", $"{user.Id}.docx");
            var account = _bankAccountRepository.Get(id);
            var countRows = 6;
            Border slimLine = new Border(BorderStyle.Tcbs_single, BorderSize.one, 0, Color.Black);
            Border boldLine = new Border(BorderStyle.Tcbs_single, BorderSize.seven, 0, Color.Black);

            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

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

        public IActionResult UpdateAmount(long id, decimal amount)
        {

            var result = _accountPresentation.UpdateAmountResult(id, amount);

            return Json(JsonConvert.DeserializeObject(result));
        }

        public IActionResult FreezeAccount(long id)
        {
            var result = _accountPresentation.AccountFreezeResult(id);

            return Json(result);
        }

        public IActionResult Transfer(long fromAccountId, string toAccountNumber, decimal transferAmount)
        {
            var result = _accountPresentation
                .GetJsonAsTransferResult(fromAccountId, toAccountNumber, transferAmount);

            return Json(JsonConvert.DeserializeObject(result));
        }
    }
}
