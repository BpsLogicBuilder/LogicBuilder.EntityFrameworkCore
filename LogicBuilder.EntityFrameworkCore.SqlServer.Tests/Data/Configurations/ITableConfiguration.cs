using Microsoft.EntityFrameworkCore;

namespace LogicBuilder.EntityFrameworkCore.SqlServer.Tests.Data.Configurations
{
    interface ITableConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
