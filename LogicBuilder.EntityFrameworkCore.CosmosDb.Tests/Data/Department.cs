using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Data
{
    public class Department : BaseDataClass
    {
        public string DepartmentID { get; set; } = "";

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = "";

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public string? InstructorID { get; set; }

        public string ETag { get; set; } = "";

        public string? AdministratorName { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}
