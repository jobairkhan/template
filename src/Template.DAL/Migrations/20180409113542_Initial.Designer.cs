﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using Template.DAL;

namespace Template.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20180409113542_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Template.Domain.Customers.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Customer","dbo");
                });

            modelBuilder.Entity("Template.Domain.Customers.PurchasedMovie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CustomerId");

                    b.Property<long?>("MovieId");

                    b.Property<DateTime>("PurchaseDate");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MovieId");

                    b.ToTable("PurchasedMovie","dbo");
                });

            modelBuilder.Entity("Template.Domain.Movies.Movie", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LicensingModel");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Movie","dbo");

                    b.HasDiscriminator<int>("LicensingModel");
                });

            modelBuilder.Entity("Template.Domain.Movies.LifeLongMovie", b =>
                {
                    b.HasBaseType("Template.Domain.Movies.Movie");


                    b.ToTable("LifeLongMovie");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Template.Domain.Movies.TwoDaysMovie", b =>
                {
                    b.HasBaseType("Template.Domain.Movies.Movie");


                    b.ToTable("TwoDaysMovie");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Template.Domain.Customers.Customer", b =>
                {
                    b.OwnsOne("Template.Domain.Customers.CustomerName", "Name", b1 =>
                        {
                            b1.Property<long>("CustomerId");

                            b1.ToTable("Customer","dbo");

                            b1.HasOne("Template.Domain.Customers.Customer")
                                .WithOne("Name")
                                .HasForeignKey("Template.Domain.Customers.CustomerName", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("Template.Domain.Customers.CustomerStatus", "Status", b1 =>
                        {
                            b1.Property<long>("CustomerId");

                            b1.ToTable("Customer","dbo");

                            b1.HasOne("Template.Domain.Customers.Customer")
                                .WithOne("Status")
                                .HasForeignKey("Template.Domain.Customers.CustomerStatus", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("Template.Domain.Customers.ExpirationDate", "ExpirationDate", b2 =>
                                {
                                    b2.Property<long>("CustomerStatusCustomerId");

                                    b2.Property<DateTime?>("Date")
                                        .HasColumnName("StatusExpirationDate");

                                    b2.ToTable("Customer","dbo");

                                    b2.HasOne("Template.Domain.Customers.CustomerStatus")
                                        .WithOne("ExpirationDate")
                                        .HasForeignKey("Template.Domain.Customers.ExpirationDate", "CustomerStatusCustomerId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });

                    b.OwnsOne("Template.Domain.Customers.Dollars", "MoneySpent", b1 =>
                        {
                            b1.Property<long>("CustomerId");

                            b1.ToTable("Customer","dbo");

                            b1.HasOne("Template.Domain.Customers.Customer")
                                .WithOne("MoneySpent")
                                .HasForeignKey("Template.Domain.Customers.Dollars", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("Template.Domain.Customers.PurchasedMovie", b =>
                {
                    b.HasOne("Template.Domain.Customers.Customer", "Customer")
                        .WithMany("PurchasedMovies")
                        .HasForeignKey("CustomerId");

                    b.HasOne("Template.Domain.Movies.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");

                    b.OwnsOne("Template.Domain.Customers.Dollars", "Price", b1 =>
                        {
                            b1.Property<long?>("PurchasedMovieId");

                            b1.ToTable("PurchasedMovie","dbo");

                            b1.HasOne("Template.Domain.Customers.PurchasedMovie")
                                .WithOne("Price")
                                .HasForeignKey("Template.Domain.Customers.Dollars", "PurchasedMovieId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("Template.Domain.Customers.ExpirationDate", "ExpirationDate", b1 =>
                        {
                            b1.Property<long?>("PurchasedMovieId");

                            b1.Property<DateTime?>("Date")
                                .HasColumnName("RentExpirationDate");

                            b1.ToTable("PurchasedMovie","dbo");

                            b1.HasOne("Template.Domain.Customers.PurchasedMovie")
                                .WithOne("ExpirationDate")
                                .HasForeignKey("Template.Domain.Customers.ExpirationDate", "PurchasedMovieId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
