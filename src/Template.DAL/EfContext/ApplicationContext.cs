using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Customers;
using Template.Domain.Movies;

namespace Template.DAL.EfContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                if (optionsBuilder.IsConfigured == false)
                {
                    optionsBuilder.UseSqlServer(
                        @"Data Source=(localdb)\\mssqllocaldb;Initial Catalog=OnlineTheater;
                       Integrated Security=True;");
                }
                base.OnConfiguring(optionsBuilder);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>(BuildCustomerMapping);

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie", "dbo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .UseSqlServerIdentityColumn()
                    .HasField("_id"); 

                entity.HasDiscriminator<int>("LicensingModel")
                    .HasValue<TwoDaysMovie>(1)
                    .HasValue<LifeLongMovie>(2);
            });

            modelBuilder.Entity<PurchasedMovie>(entity =>
            {
                entity.ToTable("PurchasedMovie", "dbo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .UseSqlServerIdentityColumn()
                    .HasField("_id"); 
                entity.OwnsOne(o => o.Price);
                entity.OwnsOne(o => o.ExpirationDate).Property(p => p.Date).HasColumnName($"RentExpirationDate");
            });

        }

        private static void BuildCustomerMapping(EntityTypeBuilder<Customer> entity)
        {
            entity.ToTable("Customer", "dbo");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .UseSqlServerIdentityColumn()
                .HasField("_id");
            entity.OwnsOne(o => o.Name, n =>
            {
                n.Property(e => e.Value)
                    .HasColumnName("Name")
                    .HasField("_value");
            });

            entity.OwnsOne(o => o.Email, n =>
            {
                n.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasField("_value");
            });

            entity.OwnsOne(o => o.MoneySpent,
                p =>
                {
                    p.Property(e => e.Value)
                        .HasColumnName("MoneySpent")
                        .HasField("_value");
                });

            entity.OwnsOne(o => o.Status, p =>
            {
                p.OwnsOne(q => q.ExpirationDate, r =>
                {
                    r.Property(s => s.Date)
                        .HasColumnName("StatusExpirationDate")
                        .HasField("_date");
                });
            });

            entity.HasMany(p => p.PurchasedMovies);
        }
    }
}
