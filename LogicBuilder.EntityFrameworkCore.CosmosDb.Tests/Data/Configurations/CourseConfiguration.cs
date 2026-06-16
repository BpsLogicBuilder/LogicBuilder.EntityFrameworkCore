using Microsoft.EntityFrameworkCore;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Configurations
{
    internal class CourseConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasKey(e => e.CourseID);
            modelBuilder.Entity<Course>().Property(e => e.CourseID).ToJsonProperty("id");
            modelBuilder.Entity<Course>().ToContainer(nameof(Course));
            modelBuilder.Entity<Course>().HasPartitionKey(c => c.DepartmentID);
        }
    }
}
