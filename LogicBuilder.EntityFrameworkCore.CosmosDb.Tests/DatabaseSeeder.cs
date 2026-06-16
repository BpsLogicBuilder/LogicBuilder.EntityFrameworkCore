using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data.Stores;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models;
using LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        internal static async Task Seed_Database(ISchoolStore repository)
        {
            if ((await repository.CountAsync<Student>()) > 0)
                return;//database has been seeded

            Instructor[] instructors =
            [
                new Instructor { ID = Guid.NewGuid().ToString(), FirstName = "Roger",   LastName = "Zheng", HireDate = DateTime.SpecifyKind(DateTime.Parse("2004-02-12", CultureInfo.CurrentCulture), DateTimeKind.Utc), EntityState = LogicBuilder.Data.EntityStateType.Added },
                new Instructor { ID = Guid.NewGuid().ToString(), FirstName = "Kim", LastName = "Abercrombie", HireDate = DateTime.SpecifyKind(DateTime.Parse("1995-03-11", CultureInfo.CurrentCulture), DateTimeKind.Utc), EntityState = LogicBuilder.Data.EntityStateType.Added},
                new Instructor { ID = Guid.NewGuid().ToString(), FirstName = "Fadi", LastName = "Fakhouri", HireDate = DateTime.SpecifyKind(DateTime.Parse("2002-07-06", CultureInfo.CurrentCulture), DateTimeKind.Utc), OfficeAssignment = new OfficeAssignment { Location = "Smith 17" }, EntityState = LogicBuilder.Data.EntityStateType.Added},
                new Instructor { ID = Guid.NewGuid().ToString(), FirstName = "Roger", LastName = "Harui", HireDate = DateTime.SpecifyKind(DateTime.Parse("1998-07-01", CultureInfo.CurrentCulture), DateTimeKind.Utc), OfficeAssignment = new OfficeAssignment { Location = "Gowan 27" }, EntityState = LogicBuilder.Data.EntityStateType.Added },
                new Instructor { ID = Guid.NewGuid().ToString(), FirstName = "Candace", LastName = "Kapoor", HireDate = DateTime.SpecifyKind(DateTime.Parse("2001-01-15", CultureInfo.CurrentCulture), DateTimeKind.Utc), OfficeAssignment = new OfficeAssignment { Location = "Thompson 304" }, EntityState = LogicBuilder.Data.EntityStateType.Added }
            ];

            await repository.SaveGraphsAsync<Instructor>(instructors);

            Department[] departments =
            [
                new Department
                {
                    DepartmentID = Guid.NewGuid().ToString(),
                    EntityState = LogicBuilder.Data.EntityStateType.Added,
                    Name = "English",     Budget = 350000,
                    StartDate = DateTime.SpecifyKind(DateTime.Parse("2007-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc),
                    InstructorID = instructors.Single(i => i.FirstName == "Kim" && i.LastName == "Abercrombie").ID,
                    AdministratorName = "Kim Abercrombie",
                    Courses =  new HashSet<Course>
                    {
                        new() {CourseID = "2021", Title = "Composition",  Credits = 3},
                        new() {CourseID = "2042", Title = "Literature",  Credits = 4}
                    }
                },
                new Department
                {
                    DepartmentID = Guid.NewGuid().ToString(),
                    EntityState = LogicBuilder.Data.EntityStateType.Added,
                    Name = "Mathematics",
                    Budget = 100000,
                    StartDate = DateTime.SpecifyKind(DateTime.Parse("2007-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc),
                    InstructorID = instructors.Single(i => i.FirstName == "Fadi" && i.LastName == "Fakhouri").ID,
                    AdministratorName = "Fadi Fakhouri",
                    Courses =  new HashSet<Course>
                    {
                        new() {CourseID = "1045", Title = "Calculus",       Credits = 4},
                        new() {CourseID = "3141", Title = "Trigonometry",   Credits = 4}
                    }
                },
                new Department
                {
                    DepartmentID = Guid.NewGuid().ToString(),
                    EntityState = LogicBuilder.Data.EntityStateType.Added,
                    Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.SpecifyKind(DateTime.Parse("2007-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc),
                    InstructorID = instructors.Single(i => i.FirstName == "Roger" && i.LastName == "Harui").ID,
                    AdministratorName = "Roger Harui",
                    Courses =  new HashSet<Course>
                    {
                        new() {CourseID = "1050", Title = "Chemistry",      Credits = 3}
                    }
                },
                new Department
                {
                    DepartmentID = Guid.NewGuid().ToString(),
                    EntityState = LogicBuilder.Data.EntityStateType.Added,
                    Name = "Economics",
                    Budget = 100000,
                    StartDate = DateTime.SpecifyKind(DateTime.Parse("2007-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc),
                    InstructorID = instructors.Single(i => i.FirstName == "Candace" && i.LastName == "Kapoor").ID,
                    AdministratorName = "Candace Kapoor",
                    Courses =  new HashSet<Course>
                    {
                        new() {CourseID = "4022", Title = "Microeconomics", Credits = 3},
                        new() {CourseID = "4041", Title = "Macroeconomics", Credits = 3 }
                    }
                }
            ];

            await repository.SaveGraphsAsync<Department>(departments);
            repository.ClearChangeTracker();


            IEnumerable<Course> courses = departments.SelectMany(d => d.Courses!);
            Instructor instructor = instructors.Single(i => i.LastName == "Kapoor");
            instructor.EntityState = LogicBuilder.Data.EntityStateType.Modified;
            instructor.Courses = new HashSet<CourseAssignment>
            {
                new() { CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID, CourseTitle = "Chemistry" },
            };

            instructor = instructors.Single(i => i.LastName == "Harui");
            instructor.EntityState = LogicBuilder.Data.EntityStateType.Modified;
            instructor.Courses = new HashSet<CourseAssignment>
            {
                new() { CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID, CourseTitle = "Chemistry" },
            };

            instructor = instructors.Single(i => i.LastName == "Zheng");
            instructor.EntityState = LogicBuilder.Data.EntityStateType.Modified;
            instructor.Courses = new HashSet<CourseAssignment>
            {
                new() { CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID, CourseTitle = "Microeconomics" },
                new() { CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID, CourseTitle = "Macroeconomics" },
            };

            instructor = instructors.Single(i => i.LastName == "Fakhouri");
            instructor.EntityState = LogicBuilder.Data.EntityStateType.Modified;
            instructor.Courses = new HashSet<CourseAssignment>
            {
                new() { CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID, CourseTitle = "Calculus" },
            };

            instructor = instructors.Single(i => i.LastName == "Harui");
            instructor.EntityState = LogicBuilder.Data.EntityStateType.Modified;
            instructor.Courses = new HashSet<CourseAssignment>
            {
                new() { CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID, CourseTitle = "Trigonometry" },
            };

            instructor = instructors.Single(i => i.LastName == "Abercrombie");
            instructor.EntityState = LogicBuilder.Data.EntityStateType.Modified;
            instructor.Courses = new HashSet<CourseAssignment>
            {
                new() { CourseID = courses.Single(c => c.Title == "Composition" ).CourseID, CourseTitle = "Composition" },
                new() { CourseID = courses.Single(c => c.Title == "Literature" ).CourseID, CourseTitle = "Literature" },
            };

            await repository.SaveGraphsAsync<Instructor>(instructors);

            Student[] students =
            [
                new Student
                {
                    ID = Guid.NewGuid().ToString(),
                    EntityState =  LogicBuilder.Data.EntityStateType.Added,
                    FirstName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.SpecifyKind(DateTime.Parse("2010-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                            Grade = Data.Grade.A,
                            CourseTitle = "Chemistry"
                        },
                        new() {
                            CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                            Grade = Data.Grade.C,
                            CourseTitle = "Microeconomics"
                        },
                        new() {
                            CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                            Grade = Data.Grade.B,
                            CourseTitle = "Macroeconomics"
                        }
                    }
                },
                new Student
                {
                    ID = Guid.NewGuid().ToString(),
                    EntityState =  LogicBuilder.Data.EntityStateType.Added,
                    FirstName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.SpecifyKind(DateTime.Parse("2012-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                            Grade = Data.Grade.B,
                            CourseTitle = "Calculus"
                        },
                        new() {
                            CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                            Grade = Data.Grade.B,
                            CourseTitle = "Trigonometry"
                        },
                        new() {
                            CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                            Grade = Data.Grade.B,
                            CourseTitle = "Composition"
                        }
                    }
                },
                new Student
                {
                    ID = Guid.NewGuid().ToString(),
                    EntityState =  LogicBuilder.Data.EntityStateType.Added,
                    FirstName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.SpecifyKind(DateTime.Parse("2013-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                            CourseTitle = "Chemistry"
                        },
                        new() {
                            CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                            Grade = Data.Grade.B,
                            CourseTitle = "Microeconomics"
                        },
                    }
                },
                new Student
                {
                    ID = Guid.NewGuid().ToString(),
                    EntityState =  LogicBuilder.Data.EntityStateType.Added,
                    FirstName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.SpecifyKind(DateTime.Parse("2012-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                            Grade = Data.Grade.B,
                            CourseTitle = "Chemistry"
                        }
                    }
                },
                new Student
                {
                    ID = Guid.NewGuid().ToString(),
                    EntityState =  LogicBuilder.Data.EntityStateType.Added,
                    FirstName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.SpecifyKind(DateTime.Parse("2012-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                            Grade = Data.Grade.B,
                            CourseTitle = "Composition"
                        }
                    }
                },
                new Student
                {
                    ID = Guid.NewGuid().ToString(),
                    EntityState =  LogicBuilder.Data.EntityStateType.Added,
                    FirstName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.SpecifyKind(DateTime.Parse("2011-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                            Grade = Data.Grade.B,
                            CourseTitle = "Literature"
                        }
                    }
                },
                new Student
                {
                    ID = Guid.NewGuid().ToString(),
                    EntityState =  LogicBuilder.Data.EntityStateType.Added,
                    FirstName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.SpecifyKind(DateTime.Parse("2013-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc)
                },
                new Student
                {
                    ID = Guid.NewGuid().ToString(),
                    EntityState = LogicBuilder.Data.EntityStateType.Added,
                    FirstName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.SpecifyKind(DateTime.Parse("2005-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc)
                },
                new Student
                {
                    ID = Guid.NewGuid().ToString(),
                    EntityState = LogicBuilder.Data.EntityStateType.Added,
                    FirstName = "Tom",
                    LastName = "Spratt",
                    EnrollmentDate = DateTime.SpecifyKind(DateTime.Parse("2010-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Calculus").CourseID,
                            Grade = Data.Grade.B,
                            CourseTitle = "Calculus"
                        }
                    }
                },
                new Student
                {
                    ID = Guid.NewGuid().ToString(),
                    EntityState = LogicBuilder.Data.EntityStateType.Added,
                    FirstName = "Billie",
                    LastName = "Spratt",
                    EnrollmentDate = DateTime.SpecifyKind(DateTime.Parse("2010-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                            Grade = Data.Grade.B,
                            CourseTitle = "Chemistry"
                        }
                    }
                },
                new Student
                {
                    ID = Guid.NewGuid().ToString(),
                    EntityState = LogicBuilder.Data.EntityStateType.Added,
                    FirstName = "Jackson",
                    LastName = "Spratt",
                    EnrollmentDate = DateTime.SpecifyKind(DateTime.Parse("2017-09-01", CultureInfo.CurrentCulture), DateTimeKind.Utc),
                    Enrollments = new HashSet<Enrollment>
                    {
                        new() {
                            CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                            Grade = Data.Grade.B,
                            CourseTitle = "Composition"
                        }
                    }
                }
            ];

            await repository.SaveGraphsAsync<Student>(students);
        }
    }
}