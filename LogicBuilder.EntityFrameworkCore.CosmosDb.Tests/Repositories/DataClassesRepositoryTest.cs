using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.AutoMapperProfiles;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Stores;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models.Repositories;
using LogicBuilder.Expressions.Utils.Expansions;
using LogicBuilder.Expressions.Utils.Strutures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Repositories
{
    public class DataClassesRepositoryTest : IClassFixture<DatabaseFixture>
    {
        static DataClassesRepositoryTest()
        {
            InitializeMapperConfiguration();
        }

        public DataClassesRepositoryTest(DatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private readonly DatabaseFixture databaseFixture;
        #endregion Fields

        [Fact]
        public async Task CanUseQueryAsync_ToGetFullProductDocumentWithAllExpansions()
        {
            //arrange
            IDataClassesRepository repository = serviceProvider.GetRequiredService<IDataClassesRepository>();

            //act
            List<ProductModel> products = await repository.QueryAsync<ProductModel, Product, List<ProductModel>, List<Product>>(q => q.OrderBy(p => p.ProductName).ToListAsync(CancellationToken.None).GetAwaiter().GetResult());

            //assert
            Assert.True(products.Count > 0);
            Assert.Empty(products[0].AlternateAddresses!);
            Assert.Equal("ProductFive", products[0].ProductName);
            Assert.Equal("CategoryTwo", products[0].Category!.CategoryName);
            Assert.Equal("E", products[0].SupplierAddress!.City);
            var productFourCities = products[1].AlternateAddresses!.Select(a => a.City).ToArray();
            Assert.Contains("CityThree", productFourCities);
            Assert.Contains("CityFour", productFourCities);
            Assert.Contains("CityFive", productFourCities);
        }

        [Fact]
        public async Task GetQuery_ExpandsMembersWithOwnedEntity()
        {
            //arrange
            IDataClassesRepository repository = serviceProvider.GetRequiredService<IDataClassesRepository>();

            //act
            var products = (await repository.GetAsync<ProductModel, Product>(p => p.ProductName == "ProductFive", q => q.OrderBy(p => p.ProductName), null)).ToArray();

            //assert
            Assert.Single(products);
            Assert.Equal("ProductFive", products[0].ProductName);
            Assert.NotNull(products[0].Category);
            Assert.Equal(2, products[0].Category!.CategoryID);
            Assert.Equal("CategoryTwo", products[0].Category!.CategoryName);
            Assert.Null(products[0].SupplierAddress);
            Assert.Null(products[0].AlternateAddresses!);
            Assert.Empty(products[0].AlternateAddressCities!);//literal lists expanded by default
        }

        [Fact]
        public async Task CanUseGetQuery_ToExpandChildEntities()
        {
            //arrange
            IDataClassesRepository repository = serviceProvider.GetRequiredService<IDataClassesRepository>();
            var expansionDefinition = new SelectExpandDefinition
            (
                ["ProductID", "Productname", "Category", "SupplierAddress"],
                [
                    new SelectExpandItem
                    (
                        "Category",
                        null,
                        null,
                        ["CategoryName"],
                        []
                    ),
                    new SelectExpandItem
                    (
                        "SupplierAddress",
                        null,
                        new SelectExpandItemQueryFunction
                        (
                            new SortCollection
                            (
                                [
                                    new SortDescription("City", ListSortDirection.Descending)
                                ]
                            )
                        )
                    )
                ]
            );

            //act
            var products = (await repository.GetAsync<ProductModel, Product>(null, q => q.OrderBy(p => p.ProductName), expansionDefinition)).ToArray();

            //assert
            Assert.True(products.Length > 0);
            Assert.Equal("ProductFive", products[0].ProductName);
            Assert.NotNull(products[0].Category);
            Assert.Equal(2, products[0].Category!.CategoryID);
            Assert.Equal("CategoryTwo", products[0].Category!.CategoryName);
            Assert.NotNull(products[0].SupplierAddress);
            Assert.Equal(10, products[0].SupplierAddress!.AddressID);
            Assert.Equal("E", products[0].SupplierAddress!.City);

            Assert.Null(products[0].AlternateAddresses!);
            Assert.Null(products[0].AlternateAddressCities!);
        }

        [Fact]
        public async Task CanUseGetQuery_ToExpandOwnsManyIntoListOfStrings()
        {
            //arrange
            IDataClassesRepository repository = serviceProvider.GetRequiredService<IDataClassesRepository>();
            var expansionDefinition = new SelectExpandDefinition
            (
                ["ProductID", "Productname", "Category", "AlternateAddressCities"],
                [
                    new SelectExpandItem
                    (
                        "Category",
                        null,
                        null,
                        ["CategoryName"],
                        []
                    )
                ]
            );

            //act
            var products = (await repository.GetAsync<ProductModel, Product>(p => p.ProductName == "ProductFour", q => q.OrderBy(p => p.ProductName), expansionDefinition)).ToArray();

            //assert
            Assert.True(products.Length > 0);
            Assert.Equal("ProductFour", products[0].ProductName);
            Assert.NotNull(products[0].Category);
            Assert.Equal(2, products[0].Category!.CategoryID);
            Assert.Equal("CategoryTwo", products[0].Category!.CategoryName);
            Assert.Null(products[0].SupplierAddress);

            Assert.Null(products[0].AlternateAddresses!);
            Assert.NotNull(products[0].AlternateAddressCities!);
            Assert.Equal(3, products[0].AlternateAddressCities!.Count);
            Assert.Contains("CityThree", products[0].AlternateAddressCities!);
            Assert.Contains("CityFour", products[0].AlternateAddressCities!);
            Assert.Contains("CityFive", products[0].AlternateAddressCities!);
        }

        #region Helpers

        [MemberNotNull(nameof(MapperConfiguration))]
        private static void InitializeMapperConfiguration()
        {
            MapperConfiguration ??= ConfigurationHelper.GetMapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.AddProfile<DataClassesMappings>();
            });
        }

        static MapperConfiguration MapperConfiguration;

        [MemberNotNull(nameof(serviceProvider))]
        private void Initialize()
        {
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddDbContext<DataClassesContext>
                (
                    options => options.UseCosmos
                    (
                        databaseFixture.GetConnectionString(),
                        GetType().Name,
                        options => 
                        {
                            options.ConnectionMode(Microsoft.Azure.Cosmos.ConnectionMode.Gateway);
                            options.HttpClientFactory(() => databaseFixture.GetHttpClient());
                        }
                    ),
                    ServiceLifetime.Transient
                )
                .AddTransient<IDataClassesStore, DataClassesStore>()
                .AddTransient<IDataClassesRepository, DataClassesRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();

            ReCreateDataBase(serviceProvider.GetRequiredService<DataClassesContext>()).GetAwaiter().GetResult();
            DatabaseSeeder.Seed_Database(serviceProvider.GetRequiredService<IDataClassesRepository>()).GetAwaiter().GetResult();
        }

        private static async Task ReCreateDataBase(DataClassesContext context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
        #endregion Helpers
    }
}
