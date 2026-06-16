using System.ComponentModel.DataAnnotations;


namespace LogicBuilder.EntityFrameworkCore.CosmosDb.Tests.Models
{
    public class CourseModel : BaseModelClass
    {
		[Display(Name = "Number")]
        public string CourseID { get; set; } = "";

        [StringLength(50, MinimumLength = 3)]
		public string Title { get; set; } = "";

		[Range(0, 5)]
		public int Credits { get; set; }

		public string DepartmentID { get; set; } = "";
    }
}