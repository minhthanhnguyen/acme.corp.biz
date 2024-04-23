using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.EF
{
    public class AcmeCorpBizDbContext : DbContext
    {
        public AcmeCorpBizDbContext(DbContextOptions<AcmeCorpBizDbContext> options) : base(options)
        {
            var dbCreater = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

            if (dbCreater != null)
            {
                // Create the database 
                if (!dbCreater.CanConnect())
                {
                    dbCreater.Create();
                }

                // Create the database tables
                if (!dbCreater.HasTables())
                {
                    dbCreater.CreateTables();
                }
            }
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderId, od.ProductId });

            modelBuilder.Entity<OrderDetailKey>().HasNoKey();
        }
    }
}
