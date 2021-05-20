﻿using SpaceWeb.Models.Human;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceWeb.EfStuff.Model
{
    public class Employe : BaseModel
    {
        public Position Position { get; set; }
        public virtual Department Department { get; set; }
        public long ForeignKeyUser { get; set; }
        public virtual User User { get; set; }
        public virtual List<OrdersEmployes> OrdersEmployes { get; set; }
        [Column(TypeName ="money")]
        public decimal SalaryPerHour { get; set; }
    }
}