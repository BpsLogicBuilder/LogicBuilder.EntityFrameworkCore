using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.AutoMapperProfiles;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Stores;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models.Repositories;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Crud.DataStores
{
    public class SchoolStoreTest : IClassFixture<DatabaseFixture>
    {
        static SchoolStoreTest()
        {
            InitializeMapperConfiguration();
        }

        public SchoolStoreTest(DatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
            Initialize();
        }

        #region Fields
        private static IServiceProvider? serviceProvider;
        private readonly DatabaseFixture databaseFixture;
        #endregion Fields

        [Fact]
        public async Task GetStudentsAsync_WithAQueryExpression_ReturnsAllItemsInTheExpectedOrder()
        {
            //arrange
            ISchoolStore store = serviceProvider!.GetRequiredService<ISchoolStore>();

            //act
            var students = await store.GetAsync<Student>(null, q => q.OrderByDescending(s => s.LastName).ThenBy(s => s.FirstName));

            //assert
            Assert.NotEmpty(students);
            Assert.Equal("Billie", students.First().FirstName);
            Assert.All(students, s => Assert.NotNull(s.Enrollments));
        }

        #region Helpers
        [MemberNotNull(nameof(MapperConfiguration))]
        private static void InitializeMapperConfiguration()
        {
            MapperConfiguration ??= ConfigurationHelper.GetMapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();

                cfg.AddProfile<SchoolProfile>();
            });
        }

        static MapperConfiguration MapperConfiguration;

        [MemberNotNull(nameof(serviceProvider))]
        private static void InitializeServiceProvider(DatabaseFixture databaseFixture)
        {
            serviceProvider ??= new ServiceCollection()
                .AddDbContext<SchoolContext>
                (
                    options => options.UseCosmos
                    (
                        databaseFixture.GetConnectionString(),
                        nameof(SchoolStoreTest),
                        options =>
                        {
                            options.ConnectionMode(Microsoft.Azure.Cosmos.ConnectionMode.Gateway);
                            options.HttpClientFactory(() => databaseFixture.GetHttpClient());
                        }
                    ),
                    ServiceLifetime.Transient
                )
                .AddTransient<ISchoolStore, SchoolStore>()
                .AddTransient<ISchoolRepository, SchoolRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();
        }

        [MemberNotNull(nameof(serviceProvider))]
        private void Initialize()
        {
            MapperConfiguration.AssertConfigurationIsValid();
            InitializeServiceProvider(databaseFixture);

            ReCreateDataBase(serviceProvider!.GetRequiredService<SchoolContext>()).GetAwaiter().GetResult();
            DatabaseSeeder.Seed_Database(serviceProvider!.GetRequiredService<ISchoolStore>()).GetAwaiter().GetResult();
        }

        private static async Task ReCreateDataBase(SchoolContext context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
        #endregion Helpers
    }
}
