using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;
using Testcontainers.MsSql;
using Xunit;

namespace LogicBuilder.EntityFrameworkCore.SqlServer.Tests
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private MsSqlContainer _msSqlContainer;

        public string GetConnectionString(string initialCatalog) 
            => new SqlConnectionStringBuilder(_msSqlContainer.GetConnectionString())
            {
                InitialCatalog = initialCatalog
            }.ToString();

        async ValueTask IAsyncLifetime.InitializeAsync()
        {
            _msSqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2025-latest").Build();
            await _msSqlContainer.StartAsync();
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await _msSqlContainer.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
