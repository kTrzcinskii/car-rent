﻿// <auto-generated />
using System;
using AppBrowser.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AppBrowser.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AppBrowser.Model.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CarId"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ExternalCarId")
                        .HasColumnType("integer");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProductionYear")
                        .HasColumnType("integer");

                    b.Property<int>("ProviderId")
                        .HasColumnType("integer");

                    b.HasKey("CarId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("AppBrowser.Model.Rent", b =>
                {
                    b.Property<int>("RentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RentId"));

                    b.Property<int>("CarId")
                        .HasColumnType("integer");

                    b.Property<decimal>("CostPerDay")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ExternalRentId")
                        .HasColumnType("integer");

                    b.Property<decimal>("InsuranceCostPerDay")
                        .HasColumnType("numeric");

                    b.Property<int>("ProviderId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("RentId");

                    b.HasIndex("CarId");

                    b.HasIndex("UserId");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("AppBrowser.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfLicenseObtained")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AppBrowser.Model.Rent", b =>
                {
                    b.HasOne("AppBrowser.Model.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppBrowser.Model.User", null)
                        .WithMany("Rents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("AppBrowser.Model.User", b =>
                {
                    b.Navigation("Rents");
                });
#pragma warning restore 612, 618
        }
    }
}
