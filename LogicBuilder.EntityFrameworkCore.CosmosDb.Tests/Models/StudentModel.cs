using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models
{
    public class StudentModel : BaseModelClass
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
		public System.DateTime EnrollmentDate { get; set; }

		public ICollection<EnrollmentModel>? Enrollments { get; set; }

        public ICollection<string>? CourseIds { get; set; }
    }
}