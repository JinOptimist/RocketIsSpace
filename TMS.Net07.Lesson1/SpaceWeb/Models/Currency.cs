using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public enum Currency
    {
        [Display(Name = "BYN")]
        BYN = 1,
        [Display(Name = "USD")]
        USD = 2,
        [Display(Name = "EUR")]
        EUR = 3,
        PLN = 4,
        GBP = 5,
        [Display(Name = "TUG")]
        TUG = 6,
        [Display(Name = "ZIM")]
        ZIM = 7

    }
}
