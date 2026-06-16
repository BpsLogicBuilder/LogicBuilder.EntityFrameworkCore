using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models
{
    public class DepartmentModel : BaseModelClass
    {
		public string DepartmentID { get; set; } = "";

        [StringLength(50, MinimumLength = 3)]
		public string Name { get; set; } = "";

        [DataType(DataType.Currency)]
		public decimal Budget { get; set; }

		[DataType(DataType.Date)]
		public System.DateTime StartDate { get; set; }

		public string? InstructorID { get; set; }

        public string ETag { get; set; } = "";

        public string? AdministratorName { get; set; }

        public ICollection<CourseModel>? Courses { get; set; }
    }
}