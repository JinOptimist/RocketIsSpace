﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models.CustomValidationAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
            var options = Enum.GetValues(typeof(Lang));
            LangOptions = new List<SelectListItem>();
            foreach (var option in options)
            {
                var selectListItem = new SelectListItem()
                {
                    Text = option.ToString(),
                    Value = option.ToString()
                };
                LangOptions.Add(selectListItem);
            }
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
        public List<Currency> MyCurrencies { get; set; }

        public List<SelectListItem> LangOptions { get; set; }
    }
}
