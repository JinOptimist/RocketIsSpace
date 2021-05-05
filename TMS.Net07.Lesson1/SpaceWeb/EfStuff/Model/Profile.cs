using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class Profile : BaseModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public string IdentificationPassport { get; set; }
        public string PhoneNumber { get; set; }

        public string PostAddress { get; set; }

        public virtual User User { get; set; }
        public virtual long UserRef { get; set; }

    }
}
