﻿using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Repositories.IRepository
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        Department Get(string name);
    }
}