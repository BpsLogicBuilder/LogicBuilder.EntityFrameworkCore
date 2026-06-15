using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests
{
    internal static class DatabaseSeeder
    {
        internal static async Task Seed_Database(IDataClassesRepository repository)
        {
            if ((await repository.CountAsync<ProductModel, Product>()) > 0)
                return;//database has been seeded

            AddressModel[] addresses =
            [
                new AddressModel { AddressID = 1, City = "CityOne" },
                new AddressModel { AddressID = 2, City = "CityTwo" },
                new AddressModel { AddressID = 3, City = "CityThree" },
                new AddressModel { AddressID = 4, City = "CityFour" },
                new AddressModel { AddressID = 5, City = "CityFive", },
                new AddressModel { AddressID = 6, City = "A" },
                new AddressModel { AddressID = 7, City = "B" },
                new AddressModel { AddressID = 8, City = "C" },
                new AddressModel { AddressID = 9, City = "D" },
                new AddressModel { AddressID = 10, City = "E" }
            ];

            CategoryModel[] categories =
            [
                new CategoryModel 
                { 
                    CategoryID = 1,
                    CategoryName = "CategoryOne"
                },
                new CategoryModel
                {
                    CategoryID = 2,
                    CategoryName = "CategoryTwo"
                }
            ];

            AlternateAddressModel[] alternateAddresses =
            [
                new AlternateAddressModel { AlternateAddressID = 1, City = "CityOne" },
                new AlternateAddressModel { AlternateAddressID = 2, City = "CityTwo" },
                new AlternateAddressModel { AlternateAddressID = 3, City = "CityThree" },
                new AlternateAddressModel { AlternateAddressID = 4, City = "CityFour" },
                new AlternateAddressModel { AlternateAddressID = 5, City = "CityFive" },
            ];

            List<string> productOneCities = ["CityOne", "CityTwo"];
            List<string> productFourCities = ["CityThree", "CityFour", "CityFive"];

            ProductModel[] products =
            [
                new ProductModel
                {
                    ProductID = Guid.NewGuid().ToString(),
                    ProductName = "ProductOne",
                    CategoryID = categories.Single(c => c.CategoryName == "CategoryOne").CategoryID!.Value,
                    Category = new CategoryModel
                    {
                        CategoryID = 1,
                        CategoryName = "CategoryOne"
                    },
                    SupplierID = addresses.Single(a => a.City == "A").AddressID,
                    SupplierAddress = new AddressModel { AddressID = 6, City = "A" },
                    AlternateAddresses = [.. alternateAddresses.Where(a => productOneCities.Contains(a.City))],
                    EntityState = Domain.EntityStateType.Added
                },
                new ProductModel
                {
                    ProductID = Guid.NewGuid().ToString(),
                    ProductName = "ProductTwo",
                    CategoryID = categories.Single(c => c.CategoryName == "CategoryOne").CategoryID!.Value,
                    Category = new CategoryModel
                    {
                        CategoryID = 1,
                        CategoryName = "CategoryOne"
                    },
                    SupplierID  = addresses.Single(a => a.City == "B").AddressID,
                    SupplierAddress = new AddressModel { AddressID = 7, City = "B" },
                    EntityState = Domain.EntityStateType.Added
                },
                new ProductModel
                {
                    ProductID = Guid.NewGuid().ToString(),
                    ProductName = "ProductThree",
                    CategoryID = categories.Single(c => c.CategoryName == "CategoryOne").CategoryID!.Value,
                    Category = new CategoryModel
                    {
                        CategoryID = 1,
                        CategoryName = "CategoryOne"
                    },
                    SupplierID = addresses.Single(a => a.City == "B").AddressID,
                    SupplierAddress = new AddressModel { AddressID = 7, City = "B" },
                    EntityState = Domain.EntityStateType.Added
                },
                new ProductModel
                {
                    ProductID = Guid.NewGuid().ToString(),
                    ProductName = "ProductFour",
                    CategoryID = categories.Single(c => c.CategoryName == "CategoryTwo").CategoryID!.Value,
                    Category = new CategoryModel
                    {
                        CategoryID = 2,
                        CategoryName = "CategoryTwo"
                    },
                    SupplierID  = addresses.Single(a => a.City == "D").AddressID,
                    SupplierAddress = new AddressModel { AddressID = 9, City = "D" },
                    AlternateAddresses = [.. alternateAddresses.Where(a => productFourCities.Contains(a.City))],
                    EntityState = Domain.EntityStateType.Added
                },
                new ProductModel
                {
                    ProductID = Guid.NewGuid().ToString(),
                    ProductName = "ProductFive",
                    CategoryID = categories.Single(c => c.CategoryName == "CategoryTwo").CategoryID!.Value,
                    Category = new CategoryModel
                    {
                        CategoryID = 2,
                        CategoryName = "CategoryTwo"
                    },
                    SupplierID  = addresses.Single(a => a.City == "E").AddressID,
                    SupplierAddress = new AddressModel { AddressID = 10, City = "E" },
                    EntityState = Domain.EntityStateType.Added
                }
            ];

            await repository.SaveGraphsAsync<ProductModel, Product>(products);
        }
    }
}