using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models
{
    public class InstructorModel : BaseModelClass
    {
		public string ID { get; set; } = "";

        [Required]
		[StringLength(50)]
		public string LastName { get; set; } = "";

        [Required]
		[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
		public string FirstName { get; set; } = "";

        public string FullName { get; set; } = "";

        [DataType(DataType.Date)]
		[Display(Name = "Hire Date")]
		public System.DateTime HireDate { get; set; }

		public ICollection<CourseAssignmentModel>? Courses { get; set; }

		public OfficeAssignmentModel? OfficeAssignment { get; set; }
    }
}