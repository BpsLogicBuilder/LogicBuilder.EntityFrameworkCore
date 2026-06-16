using LogicBuilder.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Stores
{
    public class DataClassesContext(DbContextOptions<DataClassesContext> options) : BaseDbContext(options)
    {
        public DbSet<Product> Products { get; set; }

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
