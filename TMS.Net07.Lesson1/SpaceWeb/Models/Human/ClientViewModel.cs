
using System.Collections.Generic;

namespace SpaceWeb.Models.Human
{
    public class ClientViewModel
    {
        public long Id { get; set; }
        public List<HumanOrderViewModel> Orders { get; set; }
    }
}