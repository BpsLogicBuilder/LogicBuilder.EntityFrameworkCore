using Microsoft.EntityFrameworkCore;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Configurations
{
    internal class StudentConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(e => e.ID);
            modelBuilder.Entity<Student>().Property(e => e.ID).ToJsonProperty("id");
            modelBuilder.Entity<Student>().ToContainer(nameof(Student));
            modelBuilder.Entity<Student>().HasPartitionKey(c => c.LastName);
        }
    }
}
