using LogicBuilder.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Stores
{
    public class SchoolContext(DbContextOptions<SchoolContext> options) : BaseDbContext(options)
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (PropertyInfo property in this.GetType().GetProperties().Where(p => p.PropertyType.Name == "DbSet`1"))
            {
                Type modelType = property.PropertyType.GetGenericArguments()[0];
                if (!typeof(BaseData).IsAssignableFrom(modelType))
                    continue;

                modelBuilder.Entity(modelType).Ignore(nameof(BaseData.EntityState));
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
