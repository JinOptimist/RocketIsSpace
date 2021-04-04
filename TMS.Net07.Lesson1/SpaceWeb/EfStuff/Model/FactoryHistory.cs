using System;
using Microsoft.EntityFrameworkCore;

namespace SpaceWeb.EfStuff.Model
{
    public class FactoryHistory:BaseModel
    {
        public DateTime DateOfCreating { get; set; }
        public string Creator { get; set; }
        public string RocketName { get; set; }
    }
}