using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.Models.Human
{
    public class ShortUserViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string AvatarUrl { get; set; }

        public string Login { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
    }
}
