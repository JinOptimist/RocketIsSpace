using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class SocialsPasswordViewModel
    {
        [Required]
        public string Password { get; set; }

        public string Link { get; set; }
    }
}
