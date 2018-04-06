using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Customers;
using Template.Domain.Movies;

namespace Template.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            {
                if (optionsBuilder.IsConfigured == false) {
                    optionsBuilder.UseSqlServer(
                        @"Data Source=(localdb)\\mssqllocaldb;Initial Catalog=OnlineTheater;
                       Integrated Security=True;");
                }
                base.OnConfiguring(optionsBuilder);
            }
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

    }
}
