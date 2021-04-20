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
    [Migration("20210420125835_BankAccountAddOwnerIdCheck")]
    partial class BankAccountAddOwnerIdCheck
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("BankAccount");
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

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.RocketProfile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RocketProfiles");
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

                    b.Property<int>("JobType")
                        .HasColumnType("int");

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

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.BankAccount", b =>
                {
                    b.HasOne("SpaceWeb.EfStuff.Model.User", "Owner")
                        .WithMany("BankAccounts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
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

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.Rocket", b =>
                {
                    b.Navigation("UserWhoFavouriteTheRocket");
                });

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.User", b =>
                {
                    b.Navigation("BankAccounts");

                    b.Navigation("MyRockets");

                    b.Navigation("Profile");

                    b.Navigation("TestedRockets");
                });
#pragma warning restore 612, 618
        }
    }
}
