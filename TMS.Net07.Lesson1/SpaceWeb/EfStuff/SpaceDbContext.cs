using Microsoft.EntityFrameworkCore;
using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SpaceWeb.EfStuff
{
    public class SpaceDbContext : DbContext
    {
        public SpaceDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Rocket> Rockets { get; set; }
        public DbSet<Profile> UserProfile { get; set; }
    }
}
