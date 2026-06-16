using System.ComponentModel.DataAnnotations;


namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models
{
    public class EnrollmentModel
    {
		public string CourseID { get; set; } = "";

        public Grade? Grade { get; set; }

        public string GradeLetter { get; set; } = "";

        public string CourseTitle { get; set; } = "";
    }
}