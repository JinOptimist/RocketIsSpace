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
        public DbSet<AdvImage> AdvImages { get; set; }
        public DbSet<FactoryHistory> FactoryHistories { get; set; }
        public DbSet<Comfort> Comforts { get; set; }
        public DbSet<RocketStage> RocketStages { get; set; }

        public DbSet<Addition> Additions { get; set; }
        public DbSet<AddShopRocket> AddShopRocket { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderList> OrderLists { get; set; }


        public DbSet<Order> Orders { get; set; }
        public DbSet<ComfortStructureDBmodel> ComfortsOrder { get; set; }
        public DbSet<AdditionStructureDBmodel> AdditionsOrder { get; set; }
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

            modelBuilder.Entity<Client>()
                .HasOne(client => client.User)
                .WithOne(user => user.Client)
                .HasForeignKey<User>(user => user.ClientForeignKey);

            modelBuilder.Entity<Employe>()
                .HasOne(employe => employe.User)
                .WithOne(user => user.Employe)
                .HasForeignKey<User>(user => user.EmployeForeignKey);


            modelBuilder.Entity<User>()
                .HasMany(x => x.BankAccounts)
                .WithOne(x => x.Owner);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Profile)
                .WithOne(x => x.User)
                .HasForeignKey<Profile>(x => x.UserRef);

            modelBuilder.Entity<Order>()
                .HasMany(order => order.AdditionsList)
                .WithOne(addition => addition.Order);
            modelBuilder.Entity<Order>()
                .HasMany(order => order.ComfortsList)
                .WithOne(comforts => comforts.Order);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
