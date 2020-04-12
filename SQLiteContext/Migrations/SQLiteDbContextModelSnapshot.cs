﻿// <auto-generated />
using System;
using CarRent.Contexts.SQLiteContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarRent.Contexts.SQLiteContext.Migrations
{
    [DbContext(typeof(SQLiteDbContext))]
    partial class SQLiteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("CarRent.Contexts.Models.Core.Car", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(60);

                    b.Property<int>("Colour")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<string>("EngineDescription")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasMaxLength(10)
                        .HasDefaultValue("AAA-000");

                    b.Property<double>("Mileage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("REAL")
                        .HasDefaultValue(0.0);

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(60);

                    b.Property<string>("PremiseId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenantId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PremiseId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CarRent.Contexts.Models.Core.Premise", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(300);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Premises");
                });

            modelBuilder.Entity("CarRent.Contexts.Models.Core.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(300);

                    b.Property<string>("CarId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CarId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CarRent.Contexts.Models.Core.Car", b =>
                {
                    b.HasOne("CarRent.Contexts.Models.Core.Premise", "Premise")
                        .WithMany("Cars")
                        .HasForeignKey("PremiseId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("CarRent.Contexts.Models.Core.User", b =>
                {
                    b.HasOne("CarRent.Contexts.Models.Core.Car", "RentedCar")
                        .WithOne("Tenant")
                        .HasForeignKey("CarRent.Contexts.Models.Core.User", "CarId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
