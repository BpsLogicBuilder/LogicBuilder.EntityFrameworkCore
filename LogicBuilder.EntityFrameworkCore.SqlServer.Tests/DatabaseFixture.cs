using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;
using Testcontainers.MsSql;
using Xunit;

namespace LogicBuilder.EntityFrameworkCore.SqlServer.Tests
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private MsSqlContainer? _msSqlContainer;

        public string GetConnectionString(string initialCatalog)
        {
            if (_msSqlContainer == null)
                throw new InvalidOperationException("Container is not initialized.");

            return new SqlConnectionStringBuilder(_msSqlContainer.GetConnectionString())
            {
                InitialCatalog = initialCatalog
            }.ToString();
        }

        async ValueTask IAsyncLifetime.InitializeAsync()
        {
            _msSqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server@sha256:d975fb1ea4c25c95309d7813b9f9133cde880fda16da24ab1379e277a9b2395b")//2026-latest
                                .Build();
            await _msSqlContainer.StartAsync();
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (_msSqlContainer != null)
                await _msSqlContainer.DisposeAsync();

            GC.SuppressFinalize(this);
        }
    }
}
