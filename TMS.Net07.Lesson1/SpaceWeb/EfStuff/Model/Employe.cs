﻿using System.Collections.Generic;

namespace SpaceWeb.EfStuff.Model
{
    public class Employe : BaseModel
    {
        public Specification Specification { get; set; }
        public virtual Department Department { get; set; }
        public long ForeignKeyProfile { get; set; }
        public virtual HumanProfile Profile { get; set; }
        public virtual List<OrderList> OrderList { get; set; }
        public decimal SalaryPerHour { get; set; }
    }
}