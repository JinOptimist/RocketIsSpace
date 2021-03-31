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

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.BankAccount", b =>
            {
                b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("BankAccountId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")

                    b.ToTable("BankAccounts");
            }
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

            modelBuilder.Entity("SpaceWeb.EfStuff.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<long?>("MyFavouriteRocketId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MyFavouriteRocketId");

                    b.ToTable("Users");
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
                    b.Navigation("MyRockets");

                    b.Navigation("TestedRockets");
                });
#pragma warning restore 612, 618
        }
    }
}
