using Microsoft.EntityFrameworkCore;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Configurations
{
    class ProductConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(e => e.ProductID);
            modelBuilder.Entity<Product>().Property(e => e.ProductID).ToJsonProperty("id");
            modelBuilder.Entity<Product>().ToContainer(nameof(Product));
            modelBuilder.Entity<Product>().HasPartitionKey(p => p.SupplierID);
            modelBuilder.Entity<Product>().OwnsOne(t => t.Category);
            modelBuilder.Entity<Product>().OwnsOne(t => t.SupplierAddress);
            modelBuilder.Entity<Product>().OwnsMany(t => t.AlternateAddresses);
        }
    }
}
