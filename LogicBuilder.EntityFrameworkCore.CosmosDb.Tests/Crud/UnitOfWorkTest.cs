using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.AutoMapperProfiles;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Stores;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models.Repositories;
using LogicBuilder.EntityFrameworkCore.Crud;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Crud
{
    public class UnitOfWorkTest : IClassFixture<DatabaseFixture>
    {
        static UnitOfWorkTest()
        {
            InitializeMapperConfiguration();
        }

        public UnitOfWorkTest(DatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
            Initialize();
        }

        #region Fields
        private static IServiceProvider? serviceProvider;
        private readonly DatabaseFixture databaseFixture;
        #endregion Fields

        [Fact]
        public async Task DiposingUnitOfWork_DisposesContext()
        {
            // Arrange
            SchoolContext context = serviceProvider!.GetRequiredService<SchoolContext>();
            UnitOfWork unitOfWork = new(context);
            ICollection<Student> students = await unitOfWork.GetRepository<Student>().GetAsync();
            Assert.NotEmpty(students);

            // Act
            unitOfWork.Dispose();

            // Assert
            Assert.Throws<ObjectDisposedException>(() => context.Students.ToList());
        }

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
                        nameof(UnitOfWorkTest),
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
    }
}
