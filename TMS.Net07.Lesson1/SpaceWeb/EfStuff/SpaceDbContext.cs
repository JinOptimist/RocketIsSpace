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
        public DbSet<BanksCard> BanksCard { get; set; }
        public DbSet<AdvImage> AdvImages { get; set; }
        public DbSet<FactoryHistory> FactoryHistories { get; set; }
        public DbSet<Comfort> ComfortsExample { get; set; }
        public DbSet<RocketStage> RocketStages { get; set; }

        //public DbSet<AddShopRocket> ShopRocket { get; set; }

        public DbSet<Relic> Relics { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<OrdersEmployes> OrdersEmployes { get; set; }

        public DbSet<ComfortStructure> Comforts { get; set; }
        public DbSet<AdditionStructure> Additions { get; set; }

        public DbSet<InsuranceType> InsuranceTypes { get; set; }
        public DbSet<Insurance> Insurances { get; set; }

        public DbSet<ExchangeRateToUsdCurrent> ExchangeRatesToUsdCurrent { get; set; }
        public DbSet<ExchangeRateToUsdHistory> ExchangeRatesToUsdHistory { get; set; }
        public DbSet<ExchangeAccountHistory> ExchangeAccountHistory { get; set; }

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

            modelBuilder.Entity<User>()
                .HasMany(x => x.BankAccounts)
                .WithOne(x => x.Owner)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BankAccount>()
                .HasMany(x => x.BanksCards)
                .WithOne(x => x.BankAccount);

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


            modelBuilder.Entity<Client>()
                .HasOne(x => x.User)
                .WithOne(x => x.Client)
                .HasForeignKey<Client>(x => x.ForeignKeyUser);

            modelBuilder.Entity<Employe>()
                .HasOne(x => x.User)
                .WithOne(x => x.Employe)
                .HasForeignKey<Employe>(x => x.ForeignKeyUser);

            modelBuilder.Entity<Employe>()
                .HasOne(emp => emp.Department)
                .WithMany(department => department.Employes);

            modelBuilder.Entity<OrdersEmployes>()
                .HasOne(orderList => orderList.Employe)
                .WithMany(employe => employe.OrdersEmployes);

            modelBuilder.Entity<OrdersEmployes>()
                .HasOne(orderList => orderList.Order)
                .WithMany(order => order.OrdersEmployes);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Client)
                .WithMany(client => client.Orders);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.Rockets)
                .WithMany(x => x.OrderedBy);
            

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
