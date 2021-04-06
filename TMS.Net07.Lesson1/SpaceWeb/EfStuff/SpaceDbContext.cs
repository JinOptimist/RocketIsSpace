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
        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<Relic> Relics { get; set; }
        public DbSet<AdvImage> AdvImages { get; set; }
        public DbSet<FactoryHistory> FactoryHistories { get; set; }
        public DbSet<RocketProfile> RocketProfiles { get; set; }
        public DbSet<Comfort> Comforts { get; set; }
        public DbSet<RocketStage> RocketStages { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderList> OrderLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(user => user.MyRockets)
                .WithOne(rocket => rocket.Author);

            modelBuilder.Entity<User>()
                .HasOne(user => user.MyFavouriteRocket)
                .WithMany(rocket => rocket.UserWhoFavouriteTheRocket);

            modelBuilder.Entity<Rocket>()
                .HasOne(rocket => rocket.Qa)
                .WithMany(user => user.TestedRockets);

            modelBuilder.Entity<Employe>()
                .HasOne(emp => emp.Department)
                .WithMany(department => department.Employes);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Client)
                .WithMany(client => client.Orders);

            modelBuilder.Entity<OrderList>()
                .HasOne(orderList => orderList.Employe)
                .WithMany(employe => employe.OrderList);

            modelBuilder.Entity<OrderList>()
                .HasOne(orderList => orderList.Order)
                .WithMany(order => order.OrderList);

            //modelBuilder.Entity<Client>()
            //    .HasOne(client => client.User)
            //    .WithOne(user => user.Client);

            //modelBuilder.Entity<Employe>()
            //    .HasOne(employe => employe.User)
            //    .WithOne(user => user.Employe);


            base.OnModelCreating(modelBuilder);
        }
    }
}
