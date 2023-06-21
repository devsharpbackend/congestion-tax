﻿// <auto-generated />
using System;
using Fintranet.Services.CongestionTax.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CongestionTax.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Fintranet.Services.CongestionTax.Domain.CityAggregate.City", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("WorkingCalendarId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("WorkingCalendarId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("Fintranet.Services.CongestionTax.Domain.CityAggregate.CityVehicle", b =>
                {
                    b.Property<string>("CityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VehicleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsTollFree")
                        .HasColumnType("bit");

                    b.HasKey("CityId", "VehicleId");

                    b.HasIndex("VehicleId");

                    b.ToTable("CityVehicle");
                });

            modelBuilder.Entity("Fintranet.Services.CongestionTax.Domain.CityAggregate.Tariff", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<decimal>("Charge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CityId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<TimeSpan>("FromTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("ToTime")
                        .HasColumnType("time");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Tariff");
                });

            modelBuilder.Entity("Fintranet.Services.CongestionTax.Domain.CityAggregate.WorkingCalendar", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("WorkingCalendarName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("WorkingDays")
                        .HasColumnType("int")
                        .HasColumnName("WorkingDays");

                    b.Property<string>("_holidays")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Holidays");

                    b.HasKey("Id");

                    b.ToTable("WorkingCalendar");
                });

            modelBuilder.Entity("Fintranet.Services.CongestionTax.Infrastructure.VehicleAggregate.Vehicle", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("VehicleName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("Fintranet.Services.CongestionTax.Domain.CityAggregate.City", b =>
                {
                    b.HasOne("Fintranet.Services.CongestionTax.Domain.CityAggregate.WorkingCalendar", null)
                        .WithMany()
                        .HasForeignKey("WorkingCalendarId");
                });

            modelBuilder.Entity("Fintranet.Services.CongestionTax.Domain.CityAggregate.CityVehicle", b =>
                {
                    b.HasOne("Fintranet.Services.CongestionTax.Domain.CityAggregate.City", null)
                        .WithMany("Vehicles")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fintranet.Services.CongestionTax.Infrastructure.VehicleAggregate.Vehicle", null)
                        .WithMany()
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("Fintranet.Services.CongestionTax.Domain.CityAggregate.Tariff", b =>
                {
                    b.HasOne("Fintranet.Services.CongestionTax.Domain.CityAggregate.City", null)
                        .WithMany("Tariffs")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fintranet.Services.CongestionTax.Domain.CityAggregate.City", b =>
                {
                    b.Navigation("Tariffs");

                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
