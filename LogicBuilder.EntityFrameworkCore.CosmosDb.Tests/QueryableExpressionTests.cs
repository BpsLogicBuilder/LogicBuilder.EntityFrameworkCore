using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.AutoMapperProfiles;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Stores;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models.Repositories;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using LogicBuilder.Expressions.Utils.Strutures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests
{
    public class QueryableExpressionTests : IClassFixture<DatabaseFixture>
    {
        static QueryableExpressionTests()
        {
            InitializeMapperConfiguration();
        }

        public QueryableExpressionTests(DatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
            Initialize();
        }

        #region Fields
        private static IServiceProvider? serviceProvider;
        private readonly DatabaseFixture databaseFixture;
        #endregion Fields

        [Fact]
        public async Task SelectInstructorFullNames()
        {
            //arrange
            string parameterName = "q";
            var bodyParameter = new SelectDescriptor
            (
                new OrderByDescriptor
                (
                    new ParameterDescriptor(parameterName),
                    new MemberSelectorDescriptor
                    (
                        "LastName",
                        new ParameterDescriptor("s")
                    ),
                    ListSortDirection.Ascending,
                    "s"
                ),
                new MemberSelectorDescriptor("FullName", new ParameterDescriptor("a")),
                "a"
            );

            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<InstructorModel>, IQueryable<string>>
            (
                bodyParameter,
                parameterName
            );
            var expression = GetExpression<IQueryable<InstructorModel>, IQueryable<string>>(selectorLambdaOperatorDescriptor);

            //act
            IQueryable<string> queryableResult = await serviceProvider!.GetRequiredService<ISchoolRepository>().QueryAsync<InstructorModel, Instructor, IQueryable<string>, IQueryable<string>>(expression);
            var result = await queryableResult.ToListAsync(CancellationToken.None);

            //assert
            AssertFilterStringIsCorrect(expression, "q => q.OrderBy(s => s.LastName).Select(a => a.FullName)");
            Assert.Equal("Kim Abercrombie", result[0]);
        }

        [Fact]
        public async Task Order_Instructors_Select_New_Anonymoustype_FullNameOnly()
        {
            //arrange
            string parameterName = "q";
            var bodyParameter = new SelectDescriptor
            (
                new OrderByDescriptor
                (
                    new ParameterDescriptor(parameterName),
                    new MemberSelectorDescriptor
                    (
                        "LastName",
                        new ParameterDescriptor("s")
                    ),
                    ListSortDirection.Ascending,
                    "s"
                ),
                new MemberInitDescriptor
                (
                    new Dictionary<string, DescriptorBase>
                    {
                        ["FullName"] = new MemberSelectorDescriptor("FullName", new ParameterDescriptor("a")),
                        ["LastName"] = new MemberSelectorDescriptor("LastName", new ParameterDescriptor("a"))
                    }
                ),
                "a"
            );

            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<InstructorModel>, IQueryable<dynamic>>
            (
                bodyParameter,
                parameterName
            );
            var expression = GetExpression<IQueryable<InstructorModel>, IQueryable<dynamic>>(selectorLambdaOperatorDescriptor);

            //act
            IQueryable<dynamic> queryableResult = await serviceProvider!.GetRequiredService<ISchoolRepository>().QueryAsync<InstructorModel, Instructor, IQueryable<dynamic>, IQueryable<dynamic>>(expression);
            var result = await queryableResult.ToListAsync(CancellationToken.None);

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.OrderBy(s => s.LastName).Select(a => new AnonymousType() {FullName = a.FullName, LastName = a.LastName}))");
            Assert.Equal("Kim Abercrombie", result[0].FullName);
        }

        #region Helpers

        private static SelectorLambdaDescriptor GetExpressionDescriptor<T, TResult>(DescriptorBase selectorBody, string parameterName = "$it")
            => new
            (
                selectorBody,
                typeof(T).AssemblyQualifiedName!,
                parameterName,
                typeof(TResult).AssemblyQualifiedName
            );

        private static Expression<Func<T, TResult>> GetExpression<T, TResult>(SelectorLambdaDescriptor selectorLambdaDescriptor)
        {
            IMapper mapper = serviceProvider!.GetRequiredService<IMapper>();

            return (Expression<Func<T, TResult>>)mapper.Map<SelectorLambdaOperator>
            (
                selectorLambdaDescriptor,
                opts => opts.Items["parameters"] = new Dictionary<string, ParameterExpression>()
            ).Build();
        }

        private static void AssertFilterStringIsCorrect(Expression expression, string expected)
        {
            string resultExpression = ExpressionStringBuilder.ToString(expression);
            Assert.True(expected == resultExpression, string.Format("Expected expression '{0}' but the deserializer produced '{1}'", expected, resultExpression));
        }

        [MemberNotNull(nameof(MapperConfiguration))]
        private static void InitializeMapperConfiguration()
        {
            MapperConfiguration ??= ConfigurationHelper.GetMapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();

                cfg.AddProfile<SchoolProfile>();
                cfg.AddProfile<Mapping.ExpressionOperatorsMappingProfile>();
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
                        nameof(QueryableExpressionTests),
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