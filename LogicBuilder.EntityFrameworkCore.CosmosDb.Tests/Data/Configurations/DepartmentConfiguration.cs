using Microsoft.EntityFrameworkCore;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Configurations
{
    internal class DepartmentConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasKey(e => e.DepartmentID);
            modelBuilder.Entity<Department>().Property(e => e.DepartmentID).ToJsonProperty("id");
            modelBuilder.Entity<Department>().Property(e => e.ETag).ToJsonProperty("_etag");
            modelBuilder.Entity<Department>().ToContainer(nameof(Department));
            modelBuilder.Entity<Department>().HasPartitionKey(c => c.DepartmentID);
        }
    }
}
