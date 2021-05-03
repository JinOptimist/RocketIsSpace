using System.Collections.Generic;

namespace SpaceWeb.EfStuff.Model
{
    public class Employe : BaseModel
    {
        public Specification Specification { get; set; }
        public virtual Department Department { get; set; }
        public long ForeignKeyUser { get; set; }
        public virtual User User { get; set; }
        public virtual List<OrdersEmployes> OrdersEmployes { get; set; }
        public decimal SalaryPerHour { get; set; }
    }
}