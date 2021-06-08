using Microsoft.AspNetCore.Mvc.Rendering;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models.CustomValidationAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SpaceWeb.Service;
using SpaceWeb.EfStuff.Repositories;

namespace SpaceWeb.Models
{
    public class ProfileViewModel
    {
        //private CurrencyService _currencyService;
        //private BankAccountRepository _bankAccountRepository;
        //private UserService _userService;

        public ProfileViewModel()
        {
            //_userService = userService;

            var options = Enum.GetValues(typeof(Lang));
            LangOptions = new List<SelectListItem>();
            
            foreach (var option in options)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = option.ToString(),
                    Value = option.ToString()
                };
                if (Lang.ToString() == option.ToString())
                {
                    selectListItem.Selected = true;
                }
                LangOptions.Add(selectListItem);
            }

            //AmountOfAllAccounts();
        }

        [DisplayName("Имя пользователя")]
        [Required(ErrorMessage = "Извини, но имя обязательно")]
        public string UserName { get; set; }

        [MaxLength(3)]
        public string Password { get; set; }

        [Min(18, ErrorMessage = "Только совершеннолетние")]
        public int Age { get; set; }

        [Min(0)]
        public int Money { get; set; }

        public string Bio { get; set; }
        public DateTime DateRegistration { get; set; }

        public string AvatarUrl { get; set; }

        public List<BankAccountViewModel> MyAccounts { get; set; }

        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }

        public Currency DefaultCurrency { get; set; }
        public Currency RandomCurrency { get; set; }
        public List<Currency> MyCurrencies { get; set; }
        public Lang Lang { get; set; }
        public List<SelectListItem> LangOptions { get; set; }
        public decimal AmountAllMoneyInDefaultCurrency { get; set; }

        //public void AmountOfAllAccounts()
        //{
        //    decimal allAmountInUsd = 0;

        //    var user = _userService.GetCurrent();
        //    var accounts = _bankAccountRepository.GetBankAccounts(user.Id);

        //    foreach (var account in accounts)
        //    {
        //        var amount = _currencyService.ConvertByAlex(account.Currency, account.Amount, Currency.USD);
        //        allAmountInUsd += amount;
        //    }

        //    decimal amountAllMoneyInDefaultCurrency = 0;

        //    if (DefaultCurrency != 0)
        //    {
        //        amountAllMoneyInDefaultCurrency = _currencyService.ConvertByAlex(Currency.USD, allAmountInUsd, DefaultCurrency);
        //    }
        //}
    }
}
