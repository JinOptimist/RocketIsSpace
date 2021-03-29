using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class Profile: BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public string IdentificationPassport { get; set; }
        public string PhoneNumber { get; set; }

        public string PostAddress { get; set; }



    }
}
