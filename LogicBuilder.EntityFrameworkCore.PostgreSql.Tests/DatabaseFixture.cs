using Npgsql;
using System;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;
using Xunit;

namespace LogicBuilder.EntityFrameworkCore.PostgreSql.Tests
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private PostgreSqlContainer? _postgreSqlContainer;

        public string GetConnectionString(string database)
        {
            if (_postgreSqlContainer == null)
                throw new InvalidOperationException("Container is not initialized.");

            return new NpgsqlConnectionStringBuilder(_postgreSqlContainer.GetConnectionString())
            {
                Database = database
            }.ToString();
        }

        async ValueTask IAsyncLifetime.InitializeAsync()
        {
            _postgreSqlContainer = new PostgreSqlBuilder("postgres@sha256:96d56f7f57c6aacd1fcb908bc83b345ec5f83231ee486dd66a1baadce274db88")//18=alpine
                                        .Build();
            await _postgreSqlContainer.StartAsync();
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (_postgreSqlContainer != null)
                await _postgreSqlContainer.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
