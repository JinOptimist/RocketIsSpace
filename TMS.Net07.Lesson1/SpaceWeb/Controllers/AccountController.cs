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
        private IUserRepository _userRepository; //удалить?
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
                return View("~/Views/Bank/Account/Index.cshtml", viewModel);
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
            return View("~/Views/Bank/Account/Creation.cshtml");
        }

        [HttpPost]
        public IActionResult Creation(BankAccountViewModel viewModel)
        {
            int accountLifeTime;
            if (viewModel.Currency == Currency.BYN) //заменить двойной if
            {
                if (viewModel.Type == null)
                {
                    viewModel.Type = "Счет";
                }
                accountLifeTime = 5;
            }
            else
            {
                if (viewModel.Type == null)
                {
                    viewModel.Type = "Валютный счет";
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

            return RedirectToAction("Index", new { id });
        }

        public IActionResult DownloadLog(long id)
        {
            var webPath = _hostEnvironment.WebRootPath;
            var path = Path.Combine(webPath, "TempFile", $"{id}.docx");

            var account = _bankAccountRepository.Get(id);
            using (var doc = DocX.Create(path))
            {
                doc.InsertParagraph($"Информация по счёту {account.Type}");
                doc.InsertParagraph($"Остаток на счёту: {account.Amount}");
                doc.AddTable(3, 4);
                var t = doc.InsertTable(3, 5);
                t.Rows[1].Cells[1].Paragraphs[0].Append("hello").Color(Color.Green);
                t.Rows[0].Cells[2].Width = 150;
                //t.Rows[1].Cells[2].Width = 150;
                //t.Rows[2].Cells[2].Width = 150;
                t.Rows[1].Cells[2].FillColor = Color.Red;

                Border simpleLine = new Border(BorderStyle.Tcbs_single, BorderSize.one, 0, Color.Black);
                //t.SetBorder(TableBorderType.InsideV, c);
                t.SetBorder(TableBorderType.InsideH, simpleLine);
                t.SetBorder(TableBorderType.InsideV, simpleLine);
                t.SetBorder(TableBorderType.Bottom, simpleLine);
                t.SetBorder(TableBorderType.Top, simpleLine);
                t.SetBorder(TableBorderType.Left, simpleLine);
                t.SetBorder(TableBorderType.Right, simpleLine);
                //Table ttt;
                //ttt.InsertRow(2);
                doc.Save();
            }

            var contentTypeDocx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            var fileName = $"{account.Type}.docx";
            return PhysicalFile(path, contentTypeDocx, fileName);
        }
    }
}
