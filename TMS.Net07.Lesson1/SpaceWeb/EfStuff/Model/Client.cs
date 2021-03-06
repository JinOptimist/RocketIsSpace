﻿using System.Collections.Generic;

namespace SpaceWeb.EfStuff.Model
{
    public class Client : BaseModel
    {
        public long ForeignKeyUser { get; set; }
        public virtual User User { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}