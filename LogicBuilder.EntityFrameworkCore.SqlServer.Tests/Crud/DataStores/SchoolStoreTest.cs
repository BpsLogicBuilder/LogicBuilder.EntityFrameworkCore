using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using LogicBuilder.EntityFrameworkCore.SqlServer.Tests.AutoMapperProfiles;
using LogicBuilder.EntityFrameworkCore.SqlServer.Tests.Data;
using LogicBuilder.EntityFrameworkCore.SqlServer.Tests.Data.Stores;
using LogicBuilder.EntityFrameworkCore.SqlServer.Tests.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LogicBuilder.EntityFrameworkCore.SqlServer.Tests.Crud.DataStores
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
        private IServiceProvider serviceProvider;
        private readonly DatabaseFixture databaseFixture;
        #endregion Fields

        [Fact]
        public async Task GetStudentsAsync_WithAQueryExpression_ReturnsAllItemsInTheExpectedOrder()
        {
            //arrange
            ISchoolStore store = serviceProvider.GetRequiredService<ISchoolStore>();

            //act
            var students = await store.GetAsync<Student>(null, q => q.OrderByDescending(s => s.LastName).ThenBy(s => s.FirstName));

            //assert
            Assert.NotEmpty(students);
            Assert.Equal("Billie", students.First().FirstName);
            Assert.All(students, s => Assert.Null(s.Enrollments));
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
        private void Initialize()
        {
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddDbContext<SchoolContext>
                (
                    options => options.UseSqlServer
                    (
                        databaseFixture.GetConnectionString(GetType().Name),
                        options => options.EnableRetryOnFailure()
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

            ReCreateDataBase(serviceProvider.GetRequiredService<SchoolContext>());
            DatabaseSeeder.Seed_Database(serviceProvider.GetRequiredService<ISchoolRepository>()).Wait();
        }

        private static void ReCreateDataBase(SchoolContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        #endregion Helpers
    }
}
