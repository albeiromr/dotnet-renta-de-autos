﻿// <auto-generated />
using System;
using Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240205123929_InitDb")]
    partial class InitDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Rentals.Rental", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid")
                        .HasColumnName("vehicle_id");

                    b.Property<uint>("version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_rentals");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_rentals_user_id");

                    b.HasIndex("VehicleId")
                        .HasDatabaseName("ix_rentals_vehicle_id");

                    b.ToTable("rentals", (string)null);
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("email");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.Property<uint>("version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Domain.Vehicles.Vehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Model")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("model");

                    b.Property<int[]>("PremiumAccessories")
                        .IsRequired()
                        .HasColumnType("integer[]")
                        .HasColumnName("premium_accessories");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<string>("Vin")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("vin");

                    b.Property<uint>("version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_vehicles");

                    b.ToTable("vehicles", (string)null);
                });

            modelBuilder.Entity("Domain.Rentals.Rental", b =>
                {
                    b.HasOne("Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_rentals_user_user_id");

                    b.HasOne("Domain.Vehicles.Vehicle", null)
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_rentals_vehicle_vehicle_id");

                    b.OwnsOne("Domain.Rentals.ObjectValues.DateRange", "Duration", b1 =>
                        {
                            b1.Property<Guid>("RentalId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<DateOnly>("End")
                                .HasColumnType("date")
                                .HasColumnName("duration_end");

                            b1.Property<DateOnly>("Init")
                                .HasColumnType("date")
                                .HasColumnName("duration_init");

                            b1.HasKey("RentalId");

                            b1.ToTable("rentals");

                            b1.WithOwner()
                                .HasForeignKey("RentalId")
                                .HasConstraintName("fk_rentals_rentals_id");
                        });

                    b.Navigation("Duration");
                });

            modelBuilder.Entity("Domain.Vehicles.Vehicle", b =>
                {
                    b.OwnsOne("Domain.Vehicles.ObjectValues.Location", "Location", b1 =>
                        {
                            b1.Property<Guid>("VehicleId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("location_city");

                            b1.Property<string>("PickUpAddress")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("location_pick_up_address");

                            b1.HasKey("VehicleId");

                            b1.ToTable("vehicles");

                            b1.WithOwner()
                                .HasForeignKey("VehicleId")
                                .HasConstraintName("fk_vehicles_vehicles_id");
                        });

                    b.Navigation("Location");
                });
#pragma warning restore 612, 618
        }
    }
}
