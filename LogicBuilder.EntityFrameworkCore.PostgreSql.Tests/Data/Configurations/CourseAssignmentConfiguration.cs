using Microsoft.EntityFrameworkCore;

namespace LogicBuilder.EntityFrameworkCore.PostgreSql.Tests.Data.Configurations
{
    class CourseAssignmentConfiguration : ITableConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new { c.CourseID, c.InstructorID });
        }
    }
}
