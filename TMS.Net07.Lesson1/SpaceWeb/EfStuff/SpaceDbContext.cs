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
        public DbSet<Comfort> ComfortsExample { get; set; }
        public DbSet<RocketStage> RocketStages { get; set; }

        public DbSet<Relic> Relics { get; set; }


        public DbSet<Order> Orders { get; set; }
        public DbSet<ComfortStructure> Comforts { get; set; }
        public DbSet<AdditionStructure> Additions { get; set; }
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
                .WithOne(x => x.Owner);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Profile)
                .WithOne(x => x.User)
                .HasForeignKey<Profile>(x => x.UserRef);

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
