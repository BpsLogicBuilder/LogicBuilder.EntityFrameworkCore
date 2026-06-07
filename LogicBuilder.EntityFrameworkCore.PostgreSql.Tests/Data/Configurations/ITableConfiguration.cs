using Microsoft.EntityFrameworkCore;

namespace LogicBuilder.EntityFrameworkCore.PostgreSql.Tests.Data.Configurations
{
    interface ITableConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
