using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public abstract class BaseHistoryModel : BaseModel
    {
        public DateTime DateOfChange { get; set; }
        public virtual User UserWhoChanged { get; set; }
        public string Action { get; set; }
    }
}
