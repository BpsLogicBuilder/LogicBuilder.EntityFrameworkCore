using LogicBuilder.EntityFrameworkCore.SqlServer.Tests.Data;
using LogicBuilder.EntityFrameworkCore.SqlServer.Tests.Models;
using LogicBuilder.EntityFrameworkCore.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LogicBuilder.EntityFrameworkCore.SqlServer.Tests.Visitors
{
    public class QueryFunctionUpdaterTest
    {
        [Fact]
        public void UpdaterExpansion_ShouldThrowInvalidOperationException_WhenLastExpansionDoesNotHaveQueryOption()
        {
            // Arrange
            var expansions = new List<LogicBuilder.Expressions.Utils.Expansions.ExpansionOptions>
            {
                new("AlternateAddresses", typeof(ICollection<AlternateAddressModel>), typeof(ProductModel), [], null, null),
                new("Product", typeof(ProductModel), typeof(AlternateAddressModel), [], null, null),
            };
            IQueryable<ProductModel> queryable = Enumerable.Empty<Product>().AsQueryable().Select(p => new ProductModel
            {
                AlternateAddresses = p.AlternateAddresses!.Select(i0 => new AlternateAddressModel
                {
                    Product = new ProductModel
                    {
                        AlternateAddresses = i0.Product!.AlternateAddresses!.Select(i1 => new AlternateAddressModel
                        {
                            Product = new ProductModel()
                        }).ToList()
                    }
                }).ToList()
            }).AsQueryable();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => QueryFunctionUpdater.UpdaterExpansion(queryable.Expression, expansions));
            Assert.Equal("The last expansion in the list must have a query method.", exception.Message);
        }
    }
}
