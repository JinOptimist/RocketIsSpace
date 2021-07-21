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
        private Lang _lang;

        public ProfileViewModel()
        {
            LangOptions = Enum
                .GetValues(typeof(Lang))
                .Cast<Lang>()
                .Select(option => new SelectListItem()
                {
                    Text = option.ToString(),
                    Value = option.ToString()
                }).ToList();

            // Или так
            //var options = Enum.GetValues(typeof(Lang));
            //LangOptions = new List<SelectListItem>();

            //foreach (var option in options)
            //{
            //    var selectListItem = new SelectListItem()
            //    {
            //        Text = option.ToString(),
            //        Value = option.ToString()
            //    };
            //    LangOptions.Add(selectListItem);
            //}
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
        public Lang Lang
        {
            get
            {
                return _lang;
            }
            set
            {
                _lang = value;
                LangOptions.ForEach(x => x.Selected = false);
                var option = LangOptions.SingleOrDefault(x => x.Value == _lang.ToString());
                if (option != null)
                {
                    option.Selected = true;
                }
            }
        }
        public List<SelectListItem> LangOptions { get; set; }
        public decimal AmountAllMoneyInDefaultCurrency { get; set; }

        public string AmountString { get; set; }
    }
}
