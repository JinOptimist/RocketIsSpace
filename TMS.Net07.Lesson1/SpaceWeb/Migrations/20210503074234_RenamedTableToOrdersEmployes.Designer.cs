﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpaceWeb.EfStuff;

namespace SpaceWeb.Migrations
{
    [DbContext(typeof(SpaceDbContext))]
    [Migration("20210503074234_RenamedTableToOrdersEmployes")]
    partial class RenamedTableToOrdersEmployes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.AddShopRocket", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShopRocket");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Addition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BotanicalCenterCount")
                        .HasColumnType("int");

                    b.Property<int>("ObservarionDeckCount")
                        .HasColumnType("int");

                    b.Property<int>("RescueCapsuleCount")
                        .HasColumnType("int");

                    b.Property<int>("RestRoomCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Additions");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.AdditionStructureDBmodel", b =>
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

                    b.ToTable("AdditionsOrder");
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

                    b.ToTable("Comforts");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.ComfortStructureDBmodel", b =>
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

                    b.ToTable("ComfortsOrder");
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
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Specification")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ForeignKeyUser")
                        .IsUnique();

                    b.ToTable("Employes");
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

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("JobType")
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

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.AdditionStructureDBmodel", b =>
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
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
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

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.ComfortStructureDBmodel", b =>
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

                    b.Navigation("Client");

                    b.Navigation("Employe");

                    b.Navigation("MyRockets");

                    b.Navigation("Profile");

                    b.Navigation("TestedRockets");
                });
#pragma warning restore 612, 618
        }
    }
}