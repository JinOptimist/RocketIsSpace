using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models
{
    public class AvatarViewModel
    {
        public IFormFile Avatar { get; set; }
    }
}
