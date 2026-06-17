using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using LogicBuilder.EntityFrameworkCore.Crud;
using LogicBuilder.EntityFrameworkCore.SqlServer.Tests.AutoMapperProfiles;
using LogicBuilder.EntityFrameworkCore.SqlServer.Tests.Data;
using LogicBuilder.EntityFrameworkCore.SqlServer.Tests.Data.Stores;
using LogicBuilder.EntityFrameworkCore.SqlServer.Tests.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LogicBuilder.EntityFrameworkCore.SqlServer.Tests.Crud
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
        private IServiceProvider serviceProvider;
        private readonly DatabaseFixture databaseFixture;
        #endregion Fields

        [Fact]
        public async Task DisposingUnitOfWork_DisposesContext()
        {
            // Arrange
            SchoolContext context = serviceProvider.GetRequiredService<SchoolContext>();
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
    }
}
