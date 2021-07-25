using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class AddBankCardViewModel
    {
        
        public BanksCardViewModel BanksCardViewModel {get; set; } = new BanksCardViewModel();
        public TransactionBankViewModel TransactionBankViewModel { get; set; } = new TransactionBankViewModel();
        public List<SelectListItem> CardFromDrop { get; set; }
        public List<SelectListItem> CardToDrop { get; set; }
        public void CardFromDropFill(List<BanksCard> usersCards)
        {
            CardFromDrop = new List<SelectListItem>();

            foreach (var card in usersCards)
            {
                var option = new SelectListItem();
                option.Value = card.Id.ToString();
                option.Text = card.Id.ToString();
                CardFromDrop.Add(option);
            }
        }
        public void CardToDropFill(List<BanksCard> usersCards)
        {
            CardToDrop = new List<SelectListItem>();

            foreach (var card in usersCards)
            {
                var option = new SelectListItem();
                option.Value = card.Id.ToString();
                option.Text = card.Id.ToString();
                CardToDrop.Add(option);
            }
        }
    }
}
