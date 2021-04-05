using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class RelicViewModel
    {
        public long Id { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Товара без имени не бывает")]
        public string RelicName { get; set; }

        public int Price { get; set; }

        public int Count { get; set; }
    }
}
