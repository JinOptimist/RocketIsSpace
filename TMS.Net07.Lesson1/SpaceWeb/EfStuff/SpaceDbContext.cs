using Microsoft.EntityFrameworkCore;
using SpaceWeb.EfStuff.Model;

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
