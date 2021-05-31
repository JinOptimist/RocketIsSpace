﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpaceWeb.EfStuff;

namespace SpaceWeb.Migrations
{
    [DbContext(typeof(SpaceDbContext))]
    partial class SpaceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderRocket", b =>
                {
                    b.Property<long>("OrderedById")
                        .HasColumnType("bigint");

                    b.Property<long>("RocketsId")
                        .HasColumnType("bigint");

                    b.HasKey("OrderedById", "RocketsId");

                    b.HasIndex("RocketsId");

                    b.ToTable("OrderRocket");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.AdditionStructure", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Additions");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.AdvImage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebSiteUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdvImages");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.BankAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("BankAccount");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.BanksCard", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("BankAccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("BanksCard");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Client", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ForeignKeyUser")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ForeignKeyUser")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Comfort", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("KitchenSeatsCount")
                        .HasColumnType("int");

                    b.Property<int>("SleepingCapsulesCount")
                        .HasColumnType("int");

                    b.Property<int>("StorageCapacity")
                        .HasColumnType("int");

                    b.Property<int>("ToiletCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ComfortsExample");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.ComfortStructure", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Comforts");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Department", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentType")
                        .HasColumnType("int");

                    b.Property<int>("HourEndWorking")
                        .HasColumnType("int");

                    b.Property<int>("HourStartWorking")
                        .HasColumnType("int");

                    b.Property<int>("MaximumCountEmployes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Employe", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("DepartmentId")
                        .HasColumnType("bigint");

                    b.Property<long>("ForeignKeyUser")
                        .HasColumnType("bigint");

                    b.Property<decimal>("SalaryPerHour")
                        .HasColumnType("money");

                    b.Property<int>("Specification")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ForeignKeyUser")
                        .IsUnique();

                    b.ToTable("Employes");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.ExchangeAccountHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CurrencyFrom")
                        .HasColumnType("int");

                    b.Property<int>("CurrencyTo")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExchDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ExchRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long?>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<int>("TypeOfExch")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("ExchangeAccountHistory");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.ExchangeRateToUsdCurrent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<decimal>("ExchRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TypeOfExch")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ExchangeRatesToUsdCurrent");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.ExchangeRateToUsdHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<decimal>("ExchRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ExchRateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TypeOfExch")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExchangeRatesToUsdHistory");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.FactoryHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Creator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfCreating")
                        .HasColumnType("datetime2");

                    b.Property<string>("RocketName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FactoryHistories");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Insurance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreationing")
                        .HasColumnType("datetime2");

                    b.Property<long?>("InsuranceTypeId")
                        .HasColumnType("bigint");

                    b.Property<long?>("OwnerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("InsuranceTypeId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Insurances");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.InsuranceType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("InsuranceNameType")
                        .HasColumnType("int");

                    b.Property<int>("InsurancePeriod")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("InsuranceTypes");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.OrdersEmployes", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("EmployeId")
                        .HasColumnType("bigint");

                    b.Property<long?>("OrderId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EmployeId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrdersEmployes");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Profile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentificationPassport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SurName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserRef")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserRef")
                        .IsUnique();

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Relic", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("RelicName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Relics");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Rocket", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<bool>("IsReady")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("QaId")
                        .HasColumnType("bigint");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("QaId");

                    b.ToTable("Rockets");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.RocketStage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EnginesModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FuelTanksModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RocketStageDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RocketStageModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("RocketStages");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DefaultCurrency")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("JobType")
                        .HasColumnType("int");

                    b.Property<int>("Lang")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("MyFavouriteRocketId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SurName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MyFavouriteRocketId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OrderRocket", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.Order", null)
                        .WithMany()
                        .HasForeignKey("OrderedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpaceWeb.EfStuff.Model.Rocket", null)
                        .WithMany()
                        .HasForeignKey("RocketsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.AdditionStructure", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.Order", "Order")
                        .WithMany("AdditionsList")
                        .HasForeignKey("OrderId");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.BankAccount", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.User", "Owner")
                        .WithMany("BankAccounts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.BanksCard", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.BankAccount", "BankAccount")
                        .WithMany("BanksCards")
                        .HasForeignKey("BankAccountId");

                    b.HasOne("SpaceWeb.EfStuff.Model.User", null)
                        .WithMany("BanksCards")
                        .HasForeignKey("UserId");

                    b.Navigation("BankAccount");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Client", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.User", "User")
                        .WithOne("Client")
                        .HasForeignKey("SpaceWeb.EfStuff.Model.Client", "ForeignKeyUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.ComfortStructure", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.Order", "Order")
                        .WithMany("ComfortsList")
                        .HasForeignKey("OrderId");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Employe", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.Department", "Department")
                        .WithMany("Employes")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("SpaceWeb.EfStuff.Model.User", "User")
                        .WithOne("Employe")
                        .HasForeignKey("SpaceWeb.EfStuff.Model.Employe", "ForeignKeyUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.ExchangeAccountHistory", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.User", "Owner")
                        .WithMany("ExchangeOperationsThatUserDone")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Insurance", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.InsuranceType", "InsuranceType")
                        .WithMany("WhoUseInsuranceType")
                        .HasForeignKey("InsuranceTypeId");

                    b.HasOne("SpaceWeb.EfStuff.Model.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("InsuranceType");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Order", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.OrdersEmployes", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.Employe", "Employe")
                        .WithMany("OrdersEmployes")
                        .HasForeignKey("EmployeId");

                    b.HasOne("SpaceWeb.EfStuff.Model.Order", "Order")
                        .WithMany("OrdersEmployes")
                        .HasForeignKey("OrderId");

                    b.Navigation("Employe");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Profile", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("SpaceWeb.EfStuff.Model.Profile", "UserRef")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Rocket", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.User", "Author")
                        .WithMany("MyRockets")
                        .HasForeignKey("AuthorId");

                    b.HasOne("SpaceWeb.EfStuff.Model.User", "Qa")
                        .WithMany("TestedRockets")
                        .HasForeignKey("QaId");

                    b.Navigation("Author");

                    b.Navigation("Qa");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.User", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.Rocket", "MyFavouriteRocket")
                        .WithMany("UserWhoFavouriteTheRocket")
                        .HasForeignKey("MyFavouriteRocketId");

                    b.Navigation("MyFavouriteRocket");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.BankAccount", b =>
                {
                    b.Navigation("BanksCards");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Client", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Department", b =>
                {
                    b.Navigation("Employes");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Employe", b =>
                {
                    b.Navigation("OrdersEmployes");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.InsuranceType", b =>
                {
                    b.Navigation("WhoUseInsuranceType");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Order", b =>
                {
                    b.Navigation("AdditionsList");

                    b.Navigation("ComfortsList");

                    b.Navigation("OrdersEmployes");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Rocket", b =>
                {
                    b.Navigation("UserWhoFavouriteTheRocket");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.User", b =>
                {
                    b.Navigation("BankAccounts");

                    b.Navigation("BanksCards");

                    b.Navigation("Client");

                    b.Navigation("Employe");

                    b.Navigation("ExchangeOperationsThatUserDone");

                    b.Navigation("MyRockets");

                    b.Navigation("Profile");

                    b.Navigation("TestedRockets");
                });
#pragma warning restore 612, 618
        }
    }
}
