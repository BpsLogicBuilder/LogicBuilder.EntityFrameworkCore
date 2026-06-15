using System;
using System.Net.Http;
using System.Threading.Tasks;
using Testcontainers.CosmosDb;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private readonly CosmosDbContainer _cosmosDbContainer = new CosmosDbBuilder("mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator@sha256:54d7bc334494c50cea867c270880671a7db080626a9732832b34c0d69342f9b0")//vnext-preview
                .Build();

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await _cosmosDbContainer.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        async ValueTask IAsyncLifetime.InitializeAsync()
        {
            await _cosmosDbContainer.StartAsync().ConfigureAwait(false);
        }

        public string GetConnectionString()
        {
            return _cosmosDbContainer.GetConnectionString();
        }

        public HttpClient GetHttpClient()
        {
            return _cosmosDbContainer.HttpClient;
        }
    }
}
