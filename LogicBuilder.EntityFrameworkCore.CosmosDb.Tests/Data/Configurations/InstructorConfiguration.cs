using Microsoft.EntityFrameworkCore;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Configurations
{
    internal class InstructorConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instructor>().HasKey(e => e.ID);
            modelBuilder.Entity<Instructor>().Property(e => e.ID).ToJsonProperty("id");
            modelBuilder.Entity<Instructor>().ToContainer(nameof(Instructor));
            modelBuilder.Entity<Instructor>().HasPartitionKey(c => c.LastName);
        }
    }
}
